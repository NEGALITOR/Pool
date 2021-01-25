using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Mirror.NetworkManager NM;
    public Mirror.NetworkIdentity NI;
    public Mirror.OnlineWork OW;
    public PlayerMovement PM;
    public Camera mainCam;

    public GameObject pOneCam;
    public GameObject pTwoCam;
    public CinemachineFreeLook cmflOne;
    public CinemachineFreeLook cmflTwo;

    // Start is called before the first frame update
    void Start()
    {
        NM = FindObjectOfType<Mirror.NetworkManager>();
        NI = FindObjectOfType<Mirror.NetworkIdentity>();
        OW = FindObjectOfType<Mirror.OnlineWork>();
        PM = FindObjectOfType<PlayerMovement>();

        mainCam = Camera.main;
        pOneCam = GameObject.Find("PlayerOneCam");
        pTwoCam = GameObject.Find("PlayerTwoCam");
        cmflOne = pOneCam.GetComponent<CinemachineFreeLook>();
        cmflTwo = pOneCam.GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCamera();
        
    }

    public void PlayerCamera()
    {
        if (!this.transform.parent.GetComponent<PlayerMovement>().NI.isLocalPlayer)
        {
            gameObject.GetComponent<Camera>().enabled = false;
            gameObject.GetComponent<AudioListener>().enabled = false;
        }

        if (!pOneCam.activeInHierarchy || !pTwoCam.activeInHierarchy)
        {
            return;
        }
        if (NM.numPlayers == 1)
        {
            pTwoCam.SetActive(false);
            //cmflOne.m_Follow = OW.player.transform;
            cmflOne.m_LookAt = OW.player.transform;
        }
        else if (NM.numPlayers == 2)
        {
            pOneCam.SetActive(false);
            //cmflTwo.m_Follow = OW.player.transform;
            cmflOne.m_LookAt = OW.player.transform;
        }
        else
        {
            return;
        }
    }

}
