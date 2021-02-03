using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cryoblock1 : MonoBehaviour
{
    public RayCastJump RCJ;
    public RayCastForward RCF;
    public GameObject cryoBlock;
    public List<GameObject> spawnedBlock = new List<GameObject>{ null };
    private bool charge = false;

    // Start is called before the first frame update
    void Start()
    {
        RCJ = FindObjectOfType<RayCastJump>();
        RCF = FindObjectOfType<RayCastForward>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("CryoPool"))
        {
            charge = true;
        }

        if(charge == true)
        {
            if (RCJ.currentHitObject != null && ((RCJ.currentHitObject.CompareTag("CryoPool") || RCJ.currentHitObject.CompareTag("Water")) && Input.GetKeyDown(KeyCode.E)))
            {
                foreach (GameObject block in spawnedBlock)
                {
                    IEnumerator Sub()
                    {
                        spawnedBlock[0].GetComponent<Animator>().SetBool("isActive", true);
                        yield return new WaitForSeconds(2f); //spawnedBlock[0].GetComponent<Animation>()["Cryoblock Submerge"].length
                        Destroy(block);
                    }

                    StartCoroutine(Sub());
                }
                
                spawnedBlock.Insert(0, Instantiate(cryoBlock, transform.position - new Vector3(0, 1.5f, 0), transform.rotation));
                spawnedBlock.RemoveAll(spawnedBlock => spawnedBlock == null);
                spawnedBlock[0].GetComponent<Animator>().SetBool("isActive", false);

                charge = false;
            }

        }
        else
        {
            charge = false;
            return;
        }

    }

    
}