using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public string[] playerName;
    public GameObject[] player;
    public Vector3[] playerPos;
    public Text nameA, nameB;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerName[0] = PhotonNetwork.NickName;
            photonView.RPC("Set_OtherPlayerName", RpcTarget.OthersBuffered, 0, PhotonNetwork.NickName);
            PhotonNetwork.Instantiate(player[0].name, playerPos[0], Quaternion.identity);
        }
        else
        {
            playerName[1] = PhotonNetwork.NickName;
            photonView.RPC("Set_OtherPlayerName", RpcTarget.OthersBuffered, 1, PhotonNetwork.NickName);
            Quaternion rotationB = Quaternion.Euler(0f, 180f, 0f);
            PhotonNetwork.Instantiate(player[1].name, playerPos[1], rotationB);
        }

        UpdatePlayerNames();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void Set_OtherPlayerName(int index, string name)
    {
        playerName[index] = name;
        UpdatePlayerNames();
    }

    void UpdatePlayerNames()
    {
        nameA.text = playerName[0];
        nameB.text = playerName[1];
    }
}
