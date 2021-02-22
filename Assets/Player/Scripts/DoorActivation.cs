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
            if (RCF.currentHitObject != null && RCF.currentHitObject.layer == LayerMask.NameToLayer("Chest") &&  Input.GetKeyUp(KeyCode.E))
            {
                DoorCheck();
                
            }
        }
    }

    public void DoorCheck()
    {
        if (RCF.currentHitObject.CompareTag("Chest 1"))
        {
            RCF.currentHitObject.GetComponent<Animator>().SetBool("isOpen", true);
            chestNum = "1";
            doorNum = "1";
            chest = GameObject.FindGameObjectWithTag("Chest " + chestNum);
            door = GameObject.FindGameObjectWithTag("Door " + doorNum);
            doorRb = door.GetComponent<Rigidbody>();
            StartCoroutine(DoorRotation());
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SceneSwitch"), door.transform.position - new Vector3(0, 0, -1), door.transform.rotation);
        }
        if (RCF.currentHitObject.CompareTag("Chest 2"))
        {
            RCF.currentHitObject.GetComponent<Animator>().SetBool("isOpen", true);
            chestNum = "2";
            doorNum = "2";
            chest = GameObject.FindGameObjectWithTag("Chest " + chestNum);
            door = GameObject.FindGameObjectWithTag("Door " + doorNum);
            StartCoroutine(DoorRotation());
            doorRb = door.GetComponent<Rigidbody>();
        }
    }

    IEnumerator DoorRotation()
    {
        while (door.transform.eulerAngles.y < 90)
        {
            door.transform.Rotate(0, 45f * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
