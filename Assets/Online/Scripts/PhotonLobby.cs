using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject quickJoinButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        quickJoinButton.SetActive(true);
    }

    public void OnQuickJoinButtonClicked()
    {
        Debug.Log("Quick Join Clicked");
        quickJoinButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a random game.");
        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, PublishUserId = true, MaxPlayers = (byte)MultiplayerSetting.MS.maxPlayers };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel Button Clicked");
        cancelButton.SetActive(false);
        quickJoinButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
