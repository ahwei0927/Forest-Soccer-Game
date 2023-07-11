using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Goal : MonoBehaviourPunCallbacks
{
    public Text scoreText;
    public Transform spawnPoint;
    public AudioSource audioS;
    public AudioClip win;

    public int score = 0;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if (ball != null && !ball.getStickToPlayer())
            {
                audioS.PlayOneShot(win);
                if (!photonView.IsMine)
                {
                    return;
                }

                score++;
                UpdateScore(score);

                Rigidbody ballRb = other.GetComponent<Rigidbody>();
                ballRb.velocity = Vector3.zero;
                ballRb.angularVelocity = Vector3.zero;
                other.gameObject.transform.position = spawnPoint.position;

                //Synchronize the score across all clients
                photonView.RPC("UpdateScore", RpcTarget.AllBuffered, score);
            }
        }
    }

    [PunRPC]
    public void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = score.ToString();
    }
}
