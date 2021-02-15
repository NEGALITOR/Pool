using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerTwo : MonoBehaviour
{
    public static CameraManagerTwo CMT;
    private PhotonView PV;

    public CinemachineFreeLook cmflOne;
    public CinemachineFreeLook cmflTwo;
    public GameObject pOneCam;
    public GameObject pTwoCam;

    public List<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        pOneCam = GameObject.Find("PlayerOneCam");
        pTwoCam = GameObject.Find("PlayerTwoCam");
        cmflOne = pOneCam.GetComponent<CinemachineFreeLook>();
        cmflTwo = pTwoCam.GetComponent<CinemachineFreeLook>();
    }


    private void Update()
    {
        if (PV.IsMine)
        {
            if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[0].UserId)
            {
                cmflOne.m_Priority = 2;
                cmflTwo.m_Priority = 1;
                cmflOne.m_Follow = GameObject.Find("Environment 1").transform;
                cmflOne.m_LookAt = gameObject.transform;
            }
            else if (PhotonNetwork.LocalPlayer.UserId == PhotonNetwork.PlayerList[1].UserId)
            {
                cmflOne.m_Priority = 1;
                cmflTwo.m_Priority = 2;
                cmflTwo.m_Follow = GameObject.Find("Environment 2").transform;
                cmflTwo.m_LookAt = gameObject.transform;
            }
        }
    }
}
