using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public PlayerMovement PM;
    public RayCastJump RCJ;
    public RayCastForward RCF;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PM = GetComponentInParent<PlayerMovement>();
        RCJ = GetComponentInParent<RayCastJump>();
        RCF = GetComponentInParent<RayCastForward>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonRoomCustomMatch.room.isGameLoaded)
        {
            Walking();
            Pushing();
            Jumping();            
        }
        
    }

    void Pushing()
    {
        if (RCF.currentHitObject != null && RCJ.isGrounded && RCF.currentHitObject.CompareTag("Pushable"))
        {
            anim.SetBool("isAgainstBox", true);
        }
        else
        {
            anim.SetBool("isAgainstBox", false);
        }

        if (anim.GetBool("isAgainstBox") == true && PM.direction.magnitude >= 0.1f)
        {
            anim.SetBool("isPushing", true);
        }
        else
        {
            anim.SetBool("isPushing", false);
            anim.SetBool("isAgainstBox", false);
        }
    }

    void Jumping()
    {
        if (!RCJ.isGrounded && !PM.isJumping)
        {
            anim.SetBool("isJumping", false);
        }
        else if(!RCJ.isGrounded && PM.isJumping)
        {
            anim.SetBool("isJumping", true);
        }
        else { anim.SetBool("isJumping", false); }

    }

    void Walking()
    {
        if (RCJ.isGrounded && PM.direction.magnitude >= 0.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
