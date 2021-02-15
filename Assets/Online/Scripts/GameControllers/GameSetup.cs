using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;
    private PhotonView PV;

    public Transform[] spawnPoints;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public void DisconnectPlayer()
    {
        Destroy(PhotonRoomCustomMatch.room.gameObject);
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(0);

    }

    [PunRPC]
    private void RPC_DisconnectPlayers()
    {
        PhotonNetwork.Disconnect();
    }
}