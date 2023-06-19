using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GetBallMP : MonoBehaviourPunCallbacks
{
    public Transform ball_pos;
    public KeyCode keyShoot;
    public float force = 10f;

    private GameObject ball;
    private Animator _animator;
    private PhotonView view;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        view = GetComponent<PhotonView>();

        if (!view.IsMine)
        {
            _animator.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && view.IsMine)
        {
            ball = other.gameObject;
            StartCoroutine(ReleaseBallCoroutine(transform.forward * force, Vector3.zero));
        }
    }

    private IEnumerator ReleaseBallCoroutine(Vector3 velocity, Vector3 networkVelocity)
    {
        yield return new WaitUntil(() => ball != null);

        if (view.IsMine)
        {
            ReleaseBall(velocity, networkVelocity);
        }
    }

    private void Update()
    {
        if (!view.IsMine || ball == null)
            return;

        if (view.IsMine && Input.GetButton("Fire2"))
        {
            _animator.SetBool("Kick", true);
            StartCoroutine(ResetKickAnimation());

            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            Vector3 shootDirection = transform.forward;

            ballRigidbody.AddForce(shootDirection * force, ForceMode.Impulse);

            view.RPC("ReleaseBall", RpcTarget.AllBuffered, shootDirection * force, ballRigidbody.velocity);
        }
    }

    private IEnumerator ResetKickAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        _animator.SetBool("Kick", false);
    }

    [PunRPC]
    private void ReleaseBall(Vector3 velocity, Vector3 networkVelocity)
    {
        if (ball == null)
        {
            Debug.LogError("Ball object is null in ReleaseBall method!");
            return;
        }

        ball.GetComponent<Rigidbody>().velocity = velocity;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
