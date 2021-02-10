using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerTwo : MonoBehaviour
{
    public Mirror.NetworkIdentity NI;
    public Mirror.OnlineWork OW;

    public CinemachineFreeLook cmflOne;
    public CinemachineFreeLook cmflTwo;
    public GameObject pOneCam;
    public GameObject pTwoCam;

    public List<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        NI = GetComponent<Mirror.NetworkIdentity>();
        OW = FindObjectOfType<Mirror.OnlineWork>();

        pOneCam = GameObject.Find("PlayerOneCam");
        pTwoCam = GameObject.Find("PlayerTwoCam");
        cmflOne = pOneCam.GetComponent<CinemachineFreeLook>();
        cmflTwo = pTwoCam.GetComponent<CinemachineFreeLook>();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            players.Add(player);
    }


    private void Update()
    {

        if (NI.isLocalPlayer)
        {
            

            if (gameObject.GetComponent<Mirror.NetworkIdentity>().netId == players[0].GetComponent<Mirror.NetworkIdentity>().netId)
            {
                cmflOne.m_Priority = 2;
                cmflTwo.m_Priority = 1;
                cmflOne.m_Follow = GameObject.Find("Environment 1").transform;
                cmflOne.m_LookAt = players[0].transform;
            }
            else if (gameObject.GetComponent<Mirror.NetworkIdentity>().netId == players[1].GetComponent<Mirror.NetworkIdentity>().netId)
            {
                cmflOne.m_Priority = 1;
                cmflTwo.m_Priority = 2;
                cmflTwo.m_Follow = GameObject.Find("Environment 2").transform;
                cmflTwo.m_LookAt = players[1].transform;
            }
        }
    }
}
