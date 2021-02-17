using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    //Calls
    private PhotonView PV;
    private Rigidbody rb;
    public Transform cam;
    public RayCastJump RCJ;
    public RayCastForward RCF;
    private AvatarSetup AS;

    //Player Movement
    public float pSpeed = 10f;
    public float jumpPower = 5f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public Vector3 direction;
    public bool isJumping = false;

    //[HideInInspector]
    public string hInput;
    //[HideInInspector]
    public string vInput;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        RCJ = GetComponent<RayCastJump>();
        RCF = GetComponent<RayCastForward>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AvatarSetup>();
        cam = Camera.main.transform;

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }
        Move();

    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        Jump();
        if (PhotonRoomCustomMatch.room.isGameLoaded)
        {
            WalkAnim();
            PushAnim();
            JumpAnim();
        }

    }

    void Move()
    {
        float hAxis = Input.GetAxisRaw(hInput);
        float vAxis = Input.GetAxisRaw(vInput);

        direction = new Vector3(hAxis, 0f, vAxis).normalized;
        //rb.AddForce(movementDir * pSpeed * Time.deltaTime, ForceMode.VelocityChange);


        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(moveDir.normalized * pSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }

    }

    void Jump()
    {
        if (RCJ.isGrounded && Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpPower, 0) * 50, ForceMode.Acceleration);
            RCJ.isGrounded = false;
            isJumping = true;
        }
    }

    void WalkAnim()
    {
        if (RCJ.isGrounded && direction.magnitude >= 0.1f)
        {
            AS.anim.SetBool("isWalking", true);
        }
        else
        {
            AS.anim.SetBool("isWalking", false);
        }
    }

    void JumpAnim()
    {
        if (!RCJ.isGrounded && !isJumping)
        {
            AS.anim.SetBool("isJumping", false);
        }
        else if (!RCJ.isGrounded && isJumping)
        {
            AS.anim.SetBool("isJumping", true);
        }
        else { AS.anim.SetBool("isJumping", false); }
    }

    void PushAnim()
    {
        if (RCF.currentHitObject != null && RCJ.isGrounded && RCF.currentHitObject.CompareTag("Pushable"))
        {
            AS.anim.SetBool("isAgainstBox", true);
        }
        else
        {
            AS.anim.SetBool("isAgainstBox", false);
        }

        if (AS.anim.GetBool("isAgainstBox") == true && direction.magnitude >= 0.1f)
        {
            AS.anim.SetBool("isPushing", true);
        }
        else
        {
            AS.anim.SetBool("isPushing", false);
            AS.anim.SetBool("isAgainstBox", false);
        }
    }
}