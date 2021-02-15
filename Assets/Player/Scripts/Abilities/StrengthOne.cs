using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StrengthOne : MonoBehaviour
{
    public PhotonView PV;
    public BoxCastForward BCF;

    public bool charge = false;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        BCF = GetComponent<BoxCastForward>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        if (charge == true && Input.GetKeyDown(KeyCode.E))
            if (BCF.currentHitObject != null && BCF.currentHitObject.CompareTag("Player"))
            {
                PV.RPC("RPC_StrengthWorks", RpcTarget.All);
            }
            else
            {
                charge = false;
                return;
            }

    }

    [PunRPC]
    void RPC_StrengthWorks()
    {
        if(!PV.IsMine || gameObject.GetComponent<PhotonView>().Owner == null)
        {
            Debug.Log(GetComponent<PhotonView>().Owner);
            gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            Debug.Log(GetComponent<PhotonView>().Owner);
            Debug.Log("Ownership changed");
        }
        StartCoroutine(Sub());
        charge = false;

    }

    IEnumerator Sub()
    {
        GameObject instant = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "broken wall"), this.gameObject.transform.position, this.gameObject.transform.rotation);
        instant.GetComponent<Rigidbody>().AddForce(transform.forward);
        PhotonNetwork.Destroy(this.gameObject);
        yield return new WaitForSeconds(5f);
        PhotonNetwork.Destroy(instant);
    }
    
}


/*if (BCF.currentHitObject != null && BCF.currentHitObject.CompareTag("Player"))
        {
            IEnumerator Sub()
            {
                GameObject instant = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "broken wall"), this.gameObject.transform.position, this.gameObject.transform.rotation);
                instant.GetComponent<Rigidbody>().AddForce(transform.forward);
                PhotonNetwork.Destroy(this.gameObject);
                yield return new WaitForSeconds(5f);
                Destroy(instant);
            }

            if (gameObject.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("Ownership did not changed");
                Debug.Log(gameObject.GetComponent<PhotonView>().Owner);
                Debug.Log(PhotonNetwork.MasterClient);
                StartCoroutine(Sub());
                Debug.Log(gameObject.GetComponent<PhotonView>().Owner);
            }
            else
            {
                Debug.Log("Ownership changed");
                Debug.Log(gameObject.GetComponent<PhotonView>().Owner);
                Debug.Log(PhotonNetwork.MasterClient);
                gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                StartCoroutine(Sub());
                Debug.Log(gameObject.GetComponent<PhotonView>().Owner);
            }
            charge = false;
*/