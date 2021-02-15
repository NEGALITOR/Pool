using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using System.IO;

public class Cryoblock : MonoBehaviour
{
    private PhotonView PV;
    public RayCastJump RCJ;
    public GameObject cryoBlock;
    public List<GameObject> spawnedBlock = new List<GameObject>{ null };
    public bool charge = false;
    private float step = 200f;


    public void Start()
    {
        PV = GetComponent<PhotonView>();
        RCJ = GetComponent<RayCastJump>();
    }

    public void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        CryoBlockSpawn();
    }

    public void CryoBlockSpawn()
    {
        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("CryoPool"))
        {
            charge = true;
        }

        if (charge == true)
        {
            if (RCJ.currentHitObject != null && ((RCJ.currentHitObject.CompareTag("CryoPool") || RCJ.currentHitObject.CompareTag("Water")) && Input.GetKeyUp(KeyCode.E)))
            {
                foreach (GameObject block in spawnedBlock)
                {
                    IEnumerator Sub()
                    {
                        spawnedBlock[0].GetComponent<Animator>().SetBool("isActive", true);
                        yield return new WaitForSeconds(1f); //spawnedBlock[0].GetComponent<Animation>()["Cryoblock Submerge"].length
                        Destroy(block);
                    }

                    StartCoroutine(Sub());
                }

                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObject.transform.position + new Vector3(0, 1.1f, 0), Time.deltaTime * step); ;
                spawnedBlock.Insert(0, PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CryoBlock"), transform.position - new Vector3(0, 2f, 0), transform.rotation));
                spawnedBlock[0].transform.position = Vector3.MoveTowards(spawnedBlock[0].transform.position, spawnedBlock[0].transform.position + new Vector3(0, 1f, 0), Time.deltaTime * step);
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