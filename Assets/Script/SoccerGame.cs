using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class SoccerGame : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    public Text timerText;
    public float countdownDuration = 5f;
    public float gameTimeDuration = 10f;
    public string goToScene;

    private bool isGameStarted = false;
    private string scoreGame;
    public string sceneMP;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        // Countdown
        float countdownTimer = countdownDuration;
        while (countdownTimer >= 1)
        {
            timerText.text = countdownTimer.ToString("0");
            view.RPC("UpdateTimerText", RpcTarget.All, countdownTimer.ToString("0"));
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }

        // Display "Start"
        timerText.text = "Start";
        view.RPC("UpdateTimerText", RpcTarget.All, "Start");
        isGameStarted = true;
        yield return new WaitForSeconds(1f);

        // Game Time
        float gameTimer = 0f;
        while (gameTimer <= gameTimeDuration)
        {
            if (isGameStarted)
            {
                timerText.text = FormatTime(gameTimer);
                view.RPC("UpdateTimerText", RpcTarget.All, FormatTime(gameTimer));
                gameTimer += Time.deltaTime;
            }
            yield return null;
        }

        // Game Over
        timerText.text = "Game Over";
        view.RPC("UpdateTimerText", RpcTarget.All, "Game Over");
        isGameStarted = false;
        int goalScore = FindObjectOfType<Goal>().score;
        scoreGame = (goalScore * 10).ToString(); // This is only an example calculation for the score
        Debug.Log(scoreGame);
        saveScore();
        SceneManager.LoadScene(goToScene);
    }

    [PunRPC]
    private void UpdateTimerText(string text)
    {
        timerText.text = text;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void saveScore()
    {
        FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "GS1", "add", scoreGame); // This is how we send the score to Tenenet
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (SceneManager.GetActiveScene().name == sceneMP)
            {
                if (!isGameStarted)
                {
                    managePlayerMP(false);
                }
                else
                {
                    managePlayerMP(true);
                }
            }
            else
            {
                if (!isGameStarted)
                {
                    managePlayer(false);
                }
                else
                {
                    managePlayer(true);
                }
            }
        }
    }

    [PunRPC]
    private void managePlayer(bool status)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            PlayerBall playerBall = playerObject.GetComponent<PlayerBall>();
            playerMovement.enabled = status;
            playerBall.enabled = status;
        }
    }

    [PunRPC]
    private void managePlayerMP(bool status)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PlayerMovementMP playerMovementMP = playerObject.GetComponent<PlayerMovementMP>();
            playerMovementMP.enabled = status;
        }
    }
}
