using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    private PhotonView PV;
    public RayCastJump RCJ;
    public CameraManagerTwo CMT;

    public bool charge = false;

    public List<GameObject> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        RCJ = GetComponent<RayCastJump>();
        CMT = GetComponent<CameraManagerTwo>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        InvincibilityWorks();
    }

    void InvincibilityWorks()
    {
        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("InvincibilityPool"))
        {
            IEnumerator TimeCounter()
            {
                transform.GetChild(2).gameObject.SetActive(true);
                charge = true;
                yield return new WaitForSeconds(10f);
                transform.GetChild(2).gameObject.SetActive(false);
                charge = false;
            }
            StartCoroutine(TimeCounter());
        }

        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("AcidPool") && charge == false)
        {
            gameObject.transform.position = PhotonPlayer.PP.spawnPoint.position;
        }
        else
        {
            return;
        }
    }
}
