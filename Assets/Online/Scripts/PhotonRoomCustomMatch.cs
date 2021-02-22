using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonRoomCustomMatch : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoomCustomMatch room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;
    public int multiplayerScene;

    //Player info
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playerInGame;

    //Delayed Start
    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayer;
    private float timeToStart;

    public GameObject lobbyGO;
    public GameObject roomGO;
    public Transform playersPanel;
    public GameObject playerListingPrefab;
    public GameObject startButton;

    public void Awake()
    {
        if (PhotonRoomCustomMatch.room == null)
        {
            PhotonRoomCustomMatch.room = this;
        }
        else
        {
            if (PhotonRoomCustomMatch.room != this)
            {
                Destroy(PhotonRoomCustomMatch.room.gameObject);
                PhotonRoomCustomMatch.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayer = 2;
        timeToStart = startingTime;
    }

    void Update()
    {

        if (MultiplayerSetting.MS.delayStart)
        {
            if (playersInRoom == 1)
            {
                RestartTimer();
            }
            if (!isGameLoaded)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
                else if (readyToCount)
                {
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                Debug.Log("Display time to start to the players " + timeToStart);
                if(timeToStart <= 0)
                {
                    StartGame();
                }
            }
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Entered Room");

        lobbyGO.SetActive(false);
        roomGO.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        ClearPlayerListings();
        ListPlayers();

        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        if (MultiplayerSetting.MS.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ": " + MultiplayerSetting.MS.maxPlayers + ")");
            //Replace with amount of players the room can hold
            if(playersInRoom > 1)
            {
                readyToCount = true;
            }
            if(playersInRoom == MultiplayerSetting.MS.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        /*else
        {
            StartGame();
        }*/
    }

    void ClearPlayerListings()
    {
        if(playersPanel != null) 
        {
            for (int i = playersPanel.childCount - 1; i >= 0; i--)
            {
                Destroy(playersPanel.GetChild(i).gameObject);
            }
        }
        
    }

    void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListingPrefab, playersPanel);
                TextMeshProUGUI tempText = tempListing.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                tempText.text = player.NickName;
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("New Player Joined");
        ClearPlayerListings();
        ListPlayers();
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        if (MultiplayerSetting.MS.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ": " + MultiplayerSetting.MS.maxPlayers + ")");
            if (playersInRoom > 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == MultiplayerSetting.MS.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    public void StartGame()
    {
        if(playersInRoom == MultiplayerSetting.MS.maxPlayers)
        {
            isGameLoaded = true;
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            if (MultiplayerSetting.MS.delayStart)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
            PhotonNetwork.LoadLevel(multiplayerScene);
        }
        
    }

    void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 2;
        readyToCount = false;
        readyToStart = false;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene > 0)//MultiplayerSetting.MS.multiplayerScene
        {
            isGameLoaded = true;

            if (MultiplayerSetting.MS.delayStart)
            {w
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
               RPC_CreatePlayer();
            }
        }
        /*if (currentScene == multiplayerScene)
        {
            CreatePlayer();
        }*/
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Left the game");
        playerInGame -= 2;
        StartCoroutine(DisconnectAndLoad());
        ClearPlayerListings();
        ListPlayers();
        /*if (isGameLoaded)
        {
            playerInGame -= 2;
            StartCoroutine(DisconnectAndLoad());
        }
        else
        {
            playerInGame--;
        }*/
    }


    public void DisconnectFromRoom()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        Destroy(PhotonRoomCustomMatch.room.gameObject);
        SceneManager.LoadScene(0);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playerInGame++;
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(0, 1, 0), Quaternion.identity, 0);
        Debug.Log(PhotonNetwork.LocalPlayer.UserId);
        Debug.Log(PhotonNetwork.PlayerList[0].UserId);
        Debug.Log(PhotonNetwork.PlayerList[1].UserId);
    }
}
