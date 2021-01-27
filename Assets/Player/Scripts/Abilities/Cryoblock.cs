using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cryoblock : MonoBehaviour
{
    private RayCastJump RCJ;
    private RayCastForward RCF;
    public Animator animator;
    private GameObject instant;
    public GameObject cryoBlock;
    private bool charge = false;
    public GameObject[] blockCount;

    // Start is called before the first frame update
    void Start()
    {
        RCJ = GetComponent<RayCastJump>();
        RCF = GetComponent<RayCastForward>();
        animator = cryoBlock.GetComponent<Animator>();
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
                
                instant = Instantiate(cryoBlock, transform.position - new Vector3(0, 1.1f, 0), transform.rotation);
                animator = instant.GetComponent<Animator>();
                animator.SetBool("isActive", true);
                charge = false;

                blockCount = GameObject.FindGameObjectsWithTag("CryoBlock");
                foreach (GameObject block in blockCount)
                {
                    animator.SetBool("isActive", false);
                    Destroy(block);
                }
                
            }

        }
        else
        {
            charge = false;
        }
    }
}