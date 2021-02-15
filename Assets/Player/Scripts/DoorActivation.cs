using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    public RayCastForward RCF;
    public bool isActive;
    public GameObject door;
    public GameObject chest;
    public string doorNum = "1";
    public string chestNum = "1";
    public Rigidbody doorRb;
    public List<GameObject> doorCount;

    // Start is called before the first frame update
    void Start()
    {
        RCF = GetComponent<RayCastForward>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (RCF.currentHitObject != null && RCF.currentHitObject.layer == LayerMask.NameToLayer("Chest") &&  Input.GetKeyUp(KeyCode.E))
            {
                DoorCheck();
                StartCoroutine(DoorRotation());
            }
        }
    }

    public void DoorCheck()
    {
        if (RCF.currentHitObject.CompareTag("Chest 1"))
        {
            chestNum = "1";
            doorNum = "1";
            chest = GameObject.FindGameObjectWithTag("Chest " + chestNum);
            door = GameObject.FindGameObjectWithTag("Door " + doorNum);
            doorRb = door.GetComponent<Rigidbody>();
        }
        if (RCF.currentHitObject.CompareTag("Chest 2"))
        {
            chestNum = "2";
            doorNum = "2";
            chest = GameObject.FindGameObjectWithTag("Chest " + chestNum);
            door = GameObject.FindGameObjectWithTag("Door " + doorNum);
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
