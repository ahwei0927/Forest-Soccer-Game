using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public Text[] playerName;

    public Text roomName;
    //public Text roomName;

    public GameObject roomSettingPanel;
    public GameObject waitingPanel;
    public GameObject selectPanel;

    public Button startButton;
    public Button easy, medium, hard;

    public Sprite image1, image2;

    RoomOptions options;
    public int maxNumberOfPlayers = 2;
    public int minNumberOfPlayers = 1;

    void Start()
    {
        options = new RoomOptions
        {
            MaxPlayers = (byte)maxNumberOfPlayers,
            IsOpen = true,
            IsVisible = true
        };
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == maxNumberOfPlayers)
        {
            startButton.interactable = true;
            startButton.GetComponent<Image>().sprite = image2;
            startButton.GetComponentInChildren<Text>().text = "START";

            easy.interactable = true;
            easy.GetComponent<Image>().sprite = image2;

            medium.interactable = true;
            medium.GetComponent<Image>().sprite = image2;

            hard.interactable = true;
            hard.GetComponent<Image>().sprite = image2;
        }
        else
        {
            startButton.interactable = false;
            startButton.GetComponent<Image>().sprite = image1;
            startButton.GetComponentInChildren<Text>().text = "WAITING";

            easy.interactable = false;
            easy.GetComponent<Image>().sprite = image1;

            medium.interactable = false;
            medium.GetComponent<Image>().sprite = image1;

            hard.interactable = false;
            hard.GetComponent<Image>().sprite = image1;
        }
    }

    public void JoinRoom()
    {
        if (!string.IsNullOrEmpty(roomName.text))
        {
            roomSettingPanel.SetActive(false);
            waitingPanel.SetActive(true);

            string namePlayer = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = namePlayer;
            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
        }
    }

    public void LoadScene(string sceneMP)
    {
        PhotonNetwork.LoadLevel(sceneMP);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (PhotonNetwork.IsMasterClient)
        {
            playerName[0].text = PhotonNetwork.NickName;
            photonView.RPC("Send_PlayersName", RpcTarget.OthersBuffered, 0, PhotonNetwork.NickName);
        }
        else
        {
            playerName[1].text = PhotonNetwork.NickName;
            photonView.RPC("Send_PlayersName", RpcTarget.OthersBuffered, 1, PhotonNetwork.NickName);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        throw new System.NotImplementedException();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        throw new System.NotImplementedException();
    }

    [PunRPC]
    void Send_PlayersName(int index,string name)
    {
        playerName[index].text = name;
    }

    public void activeObject()
    {
        waitingPanel.SetActive(false);
        selectPanel.SetActive(true);
    }
}
