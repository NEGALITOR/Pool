using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject otherPortal;
    private bool cooldown = false;
    public Vector3 offSet;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 offSet = new Vector3(0.0f, 0.0f, 25.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cooldown == false)
        {
            other.transform.position = otherPortal.transform.position;
            otherPortal.GetComponent<Portal>().cooldown = true;
            Invoke("CoolDown", 3.0f);
        }
    }

    void CoolDown()
    {
        otherPortal.GetComponent<Portal>().cooldown = false;
    }
}
