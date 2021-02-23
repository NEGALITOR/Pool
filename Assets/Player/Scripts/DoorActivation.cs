using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    public RayCastForward RCF;
    private PhotonView PV;
    public bool isActive;
    public GameObject door;
    public GameObject chest;
    public GameObject sceneSwitcher;
    public string doorNum = "1";
    public string chestNum = "1";
    public Rigidbody doorRb;
    public List<GameObject> doorCount;
    public float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        RCF = GetComponent<RayCastForward>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            if (RCF.currentHitObject != null && RCF.currentHitObject.layer == LayerMask.NameToLayer("Chest") &&  Input.GetKeyDown(KeyCode.E))
            {
                DoorCheck();
                
            }
        }
    }

    public void DoorCheck()
    {
        if (RCF.currentHitObject.CompareTag("Chest 1"))
        {
            chestNum = "1";
            doorNum = "1";
            PV.RPC("RPC_ChestDoor", RpcTarget.All, chestNum, doorNum);
        }
        if (RCF.currentHitObject.CompareTag("Chest 2"))
        {
            chestNum = "2";
            doorNum = "2";
            PV.RPC("RPC_ChestDoor", RpcTarget.All, chestNum, doorNum);
        }
    }

    IEnumerator DoorRotation()
    {
        while (door.transform.eulerAngles.y < (currentRotation + 90))
        {
            door.transform.Rotate(0, 45f * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    [PunRPC]
    void RPC_ChestDoor(string chestNumber, string doorNumber)
    {
        chest = GameObject.FindGameObjectWithTag("Chest " + chestNumber);
        door = GameObject.FindGameObjectWithTag("Door " + doorNumber);
        doorRb = door.GetComponent<Rigidbody>();
        if (PhotonNetwork.IsMasterClient)
        {
            chest.GetComponent<Animator>().SetBool("isOpen", true);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SceneSwitch"), door.transform.position - new Vector3(0, 0, -1), door.transform.rotation);
            currentRotation = door.transform.eulerAngles.y;
            StartCoroutine(DoorRotation());
        }
        
        
    }
}
