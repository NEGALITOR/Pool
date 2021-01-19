using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject otherPortal;
    private bool cooldown = true;
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
        if (other.CompareTag("Player") && cooldown == true)
        {
            other.transform.position = otherPortal.transform.position + offSet;
            cooldown = false;
            Invoke("CoolDown", 1.0f);
        }
    }

    void CoolDown()
    {
        cooldown = true;
    }
}
