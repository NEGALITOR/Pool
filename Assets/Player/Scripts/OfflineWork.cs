using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineWork : MonoBehaviour
{
    public Mirror.OnlineWork OW;
    public PlayerMovement PM;
    public int numPlayers;

    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        Instantiate(OW.pOne);
        Instantiate(OW.pTwo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CouchCoopControls()
    {

        if (numPlayers == 1)
        {
            PM.hInput = "Horizontal";
            PM.vInput = "Vertical";
        }
        else if (numPlayers == 2)
        {
            PM.hInput = "Horizontal2";
            PM.vInput = "Vertical2";
        }
        else
        {
            return;
        }
    }
}
