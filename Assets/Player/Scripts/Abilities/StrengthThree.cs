using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StrengthThree : MonoBehaviour
{
    private PhotonView PV;
    private RayCastForward RCF;
    private RayCastJump RCJ;

    private bool charge = false;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
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
        if (charge == true && Input.GetKeyDown(KeyCode.E))
        {
            if (RCF.currentHitObject != null && RCF.currentHitObject.CompareTag("BreakableWall"))
            {
                PV.RPC("RPC_StrengthCheck", RpcTarget.All, new object[] { RCF.currentHitObject.GetComponent<PhotonView>().ViewID });
            }
            
        }
    }

    [PunRPC]
    void RPC_StrengthCheck(int viewID)
    {
        PhotonView view = PhotonView.Find(viewID);

        if (!PhotonNetwork.IsMasterClient)
            return;
        StartCoroutine(Sub(view));
    }
    IEnumerator Sub(PhotonView v)
    {
        GameObject instant = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "broken wall"), v.transform.position, v.transform.rotation, 0);
        instant.GetComponent<Rigidbody>().AddForce(transform.forward);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(v);
        }
        yield return new WaitForSeconds(5f);
        PhotonNetwork.Destroy(instant);
    }

}
