using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StrengthTwo : MonoBehaviour
{
    public RayCastJump RCJ;
    public StrengthOne SO;

    public bool charge = false;

    // Start is called before the first frame update
    void Start()
    {
        RCJ = GetComponent<RayCastJump>();
        SO = GameObject.FindGameObjectWithTag("BreakableWall").GetComponent<StrengthOne>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RCJ.currentHitObject != null && RCJ.currentHitObject.CompareTag("StrengthPool"))
        {
            charge = true;
            SO.charge = charge;
        }
    }
}
