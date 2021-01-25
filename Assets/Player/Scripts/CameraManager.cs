using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Mirror.NetworkManager NM;
    public Mirror.NetworkIdentity NI;
    public Mirror.OnlineWork OW;
    public Mirror.NetworkBehaviour NB;
    public PlayerMovement PM;
    public Camera mainCam;

    public GameObject pOneCam;
    public GameObject pTwoCam;

    // Start is called before the first frame update
    void Start()
    {
        NM = FindObjectOfType<Mirror.NetworkManager>();
        NI = FindObjectOfType<Mirror.NetworkIdentity>();
        OW = FindObjectOfType<Mirror.OnlineWork>();
        NB = FindObjectOfType<Mirror.NetworkBehaviour>();
        PM = FindObjectOfType<PlayerMovement>();
        mainCam = Camera.main;

        pOneCam = GameObject.Find("PlayerOneCam");
        pTwoCam = GameObject.Find("PlayerTwoCam");
    }

    // Update is called once per frame
    void Update()
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
        }
        else if (NM.numPlayers == 2)
        {
            pOneCam.SetActive(false);
        }
        else
        {
            return;
        }
    }

}
