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
    private PhotonView view;

    private BallMP ballAttachedToPlayer;
    private void Start()
    {      
        view = GetComponent<PhotonView>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballAttachedToPlayer = ball.GetComponent<BallMP>();
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

        if (view.IsMine && Input.GetButton("Fire2") && ballAttachedToPlayer.getStickToPlayer())
        {

            ballAttachedToPlayer.setStickToPlayer(false);
            Rigidbody ballRigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
            Vector3 shootDirection = transform.forward;

            ballRigidbody.AddForce(shootDirection * force, ForceMode.Impulse);

            view.RPC("ReleaseBall", RpcTarget.AllBuffered, shootDirection * force, ballRigidbody.velocity);
        }
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
