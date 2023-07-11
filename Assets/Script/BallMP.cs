using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallMP : MonoBehaviour
{
    private bool stickToPlayer;
    [SerializeField] private Transform transformPlayer;
    [SerializeField] private Transform playerBallPosition;

    float speed;
    Vector3 previousLocation;
    GetBallMP playerBall;
    PhotonView view;

    private void Start()
    {
        

        playerBall = transformPlayer.GetComponent<GetBallMP>();
        view = GetComponent<PhotonView>();

        
    }

    void Update()
    {
        
            if (!stickToPlayer)
            {
                /*float distanceToPlayer = Vector3.Distance(transformPlayer.position, transform.position);
                if (distanceToPlayer < 1.5f && !playerBall.isShoot)
                {
                    Rigidbody rigidbody = GetComponent<Rigidbody>();
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
                    transform.position = new Vector3();
                    stickToPlayer = true;
                }*/
            }
            else if (stickToPlayer)
            {
                //Debug.Log("Stick To Player: "+stickToPlayer);
                Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
                speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
                transform.position = playerBallPosition.position;
                transform.Rotate(new Vector3(transformPlayer.right.x, 0, transformPlayer.right.z), speed, Space.World);
                previousLocation = currentLocation;
            }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            transformPlayer = player.transform;
            playerBallPosition = collision.gameObject.transform.Find("BallPo");
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            transform.position = playerBallPosition.position;
            setStickToPlayer(true);
        }
    }

    public void setStickToPlayer(bool value)
    {
        stickToPlayer = value;
    }
    public bool getStickToPlayer()
    {
        return stickToPlayer;
    }
}
