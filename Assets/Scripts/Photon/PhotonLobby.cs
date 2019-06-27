using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    GooglePlayGameController googlePlayGameController;
    public static PhotonLobby lobby = null;
    [SerializeField] private Button btn_Multiplayer;

    // Start is called before the first frame update
    void Start()
    {
        btn_Multiplayer.interactable = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings(); //Connect to Photon Server

        //googlePlayGameController = GameObject.Find("GooglePlayController").GetComponent<GooglePlayGameController>();
        //googlePlayGameController.SignIn();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player Connected to Server");
        btn_Multiplayer.interactable = true;
    }


    public void CreateRoom()
    {
        RoomOptions roomOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        int room = Random.Range(0, 1000);
        PhotonNetwork.CreateRoom("Room " + room.ToString(), roomOpts);

    }

    public void JoinRoom()
    {
        Debug.Log("Joining into the room...");
        btn_Multiplayer.interactable = false;

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Timeout Started");
        StartCoroutine(Timeout());
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(3f);
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        StopAllCoroutines();
        Debug.Log("On room " + PhotonNetwork.CurrentRoom);
        PhotonNetwork.LoadLevel("Core");
    }

    private void Update()
    {

    }

}
