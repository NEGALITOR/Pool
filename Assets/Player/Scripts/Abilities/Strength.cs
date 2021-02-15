using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Strength : MonoBehaviourPun
{
    private PhotonView PV;
    public RayCastForward RCF;
    public RayCastJump RCJ;

    public bool charge = false;

    public void Start()
    {
        PV = GetComponent<PhotonView>();
        RCF = GetComponent<RayCastForward>();
        RCJ = GetComponent<RayCastJump>();
    }
    public void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("StrengthPool"))
        {
            charge = true;
        }
        if (charge == true && Input.GetKeyDown(KeyCode.E))
        {
            

            if (RCF.currentHitObject != null && RCF.currentHitObject.CompareTag("BreakableWall"))
            {
                StartCoroutine(Sub());
                charge = false;
            }
            else
            {
                charge = false;
                return;
            }

            PV.RPC("RPC_WallSwitch", RpcTarget.AllBufferedViaServer, new object[] { RCF.currentHitObject.GetComponent<PhotonView>().ViewID });
        }
        
    }

    [PunRPC]
    void RPC_WallSwitch(int viewID)
    {
        PhotonView view = PhotonView.Find(viewID);

        
    }

    IEnumerator Sub()
    {
        GameObject instant = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "broken wall"), RCF.currentHitObject.transform.position, RCF.currentHitObject.transform.rotation, 0);
        instant.GetComponent<Rigidbody>().AddForce(transform.forward);
        PhotonNetwork.Destroy(RCF.currentHitObject);
        yield return new WaitForSeconds(5f);
        PhotonNetwork.Destroy(instant);
    }




    /*
     * Debug.Log("Ownership changed");
            Debug.Log(GetComponent<PhotonView>().Owner);
            Debug.Log(PhotonNetwork.MasterClient);
            RCF.currentHitObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);  
     * 
     * 
     * 
     * if (RCF.currentHitObject.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("Ownership did not changed");
                Debug.Log(RCF.currentHitObject.GetComponent<PhotonView>().Owner);
                Debug.Log(PhotonNetwork.MasterClient);
                StartCoroutine(Sub());
            }
            else
            {
                Debug.Log("Ownership changed");
                Debug.Log(RCF.currentHitObject.GetComponent<PhotonView>().Owner);
                Debug.Log(PhotonNetwork.MasterClient);
                RCF.currentHitObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                StartCoroutine(Sub());
            }*/

    /* Start is called before the first frame update
    void Start()
    {
        RCF = GetComponent<RayCastForward>();
        RCJ = GetComponent<RayCastJump>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("StrengthPool"))
        {
            charge = true;
        }

        if (charge == true)
        {
            if (RCF.currentHitObject != null && RCF.currentHitObject.CompareTag("BreakableWall") && Input.GetKeyDown(KeyCode.E))
            {
                IEnumerator Sub()
                {
                    GameObject instant = BoltNetwork.Instantiate(destroyedVer, RCF.currentHitObject.transform.position, RCF.currentHitObject.transform.rotation);
                    instant.GetComponent<Rigidbody>().AddForce(transform.forward);
                    BoltNetwork.Destroy(RCF.currentHitObject);
                    yield return new WaitForSeconds(5f);
                    Destroy(instant);
                }
                StartCoroutine(Sub());
            }
        }
        else
        {
            charge = false;
            return;
        }
    }*/

}
