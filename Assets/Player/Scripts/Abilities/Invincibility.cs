using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public RayCastJump RCJ;
    public GameObject spawnPoint;

    public bool charge = false;

    // Start is called before the first frame update
    void Start()
    {
        RCJ = FindObjectOfType<RayCastJump>();
        spawnPoint = GameObject.Find("SpawnPoint");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("InvincibilityPool"))
        {
            IEnumerator TimeCounter()
            {
                charge = true;
                yield return new WaitForSeconds(10f);
                charge = false;
            }
            StartCoroutine(TimeCounter());
        }

        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("AcidPool") && charge == false)
        {
            gameObject.transform.position = spawnPoint.transform.position;
        }
        else
        {
            return;
        }
    }
}
