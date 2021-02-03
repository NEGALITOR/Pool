using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cryoblock : MonoBehaviour
{
    public RayCastJump RCJ;
    public RayCastForward RCF;
    public GameObject player;
    public GameObject cryoBlock;
    public GameObject[] spawnedblock;
    private bool charge = false;

    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        RCJ = player.GetComponent<RayCastJump>();
        RCF = player.GetComponent<RayCastForward>();
        anim = GetComponent<Animation>();
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
                Instantiate(cryoBlock, player.transform.position - new Vector3(0, 1.1f, 0), player.transform.rotation);
                anim.Play("Cryoblock Emerge");
                

                spawnedblock = GameObject.FindGameObjectsWithTag("CryoBlock");
                foreach (GameObject block in spawnedblock)
                {
                    anim.Play("Cryoblock Submerge");
                    Destroy(block);
                }

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