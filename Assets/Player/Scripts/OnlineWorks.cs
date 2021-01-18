using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineWorks : MonoBehaviour
{
    public PlayerMovement PM;
    public Mirror.NetworkManager NM;
    public Mirror.NetworkIdentity NI;
    public int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        NM = FindObjectOfType<Mirror.NetworkManager>();
        NI = FindObjectOfType<Mirror.NetworkIdentity>();
        OnlineControls();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(NM.numPlayers);
    }

    void OnlineControls()
    {
       /*GameObject[] players = GameObject.FindGameObjectsWithTag("Player");


        foreach (GameObject player in players)
        {
            if (players.Length == 1)
            {
                playerNum = 1;
            }
            else
            {
                playerNum = 2;
            }
        }
       */

        if (NM.numPlayers == 1)
        {
            PM.hInput = "Horizontal";
            PM.vInput = "Vertical";
            
        }
        else if (NM.numPlayers == 2)
        {
            PM.hInput = "Horizontal2";
            PM.vInput = "Vertical2";
            if (!NI.isLocalPlayer)
            {
                return;
            }
        }
        else
        {
            PM.hInput = null;
            PM.vInput = null;
        }
    }
}
