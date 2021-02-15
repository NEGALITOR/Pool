using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public static PhotonPlayer PP;
    private PhotonView PV;
    public GameObject myAvatar;
    public Transform spawnPoint;

    public void OnEnable()
    {
        if (PhotonPlayer.PP == null)
        {
            PhotonPlayer.PP = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        //int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[0].UserId)
            {
                spawnPoint = GameSetup.GS.spawnPoints[0];
            }
            else if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[1].UserId)
            {
                spawnPoint = GameSetup.GS.spawnPoints[1];
            }
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawnPoint.position, spawnPoint.rotation, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
