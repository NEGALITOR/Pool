using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : MonoBehaviour
{
    public RayCastForward RCF;
    public RayCastJump RCJ;
    public GameObject destroyedVer;
    public GameObject solidVer;

    public bool charge = false;

    // Start is called before the first frame update
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
                    GameObject instant = Instantiate(destroyedVer, RCF.currentHitObject.transform.position, RCF.currentHitObject.transform.rotation);
                    instant.GetComponent<Rigidbody>().AddForce(transform.forward);
                    Destroy(RCF.currentHitObject);
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
    }
}
