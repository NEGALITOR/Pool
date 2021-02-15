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
    public RayCastJump rCastJ;

    //Player Movement
    public float pSpeed = 10f;
    public float jumpPower = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //[HideInInspector]
    public string hInput;
    //[HideInInspector]
    public string vInput;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {
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

    }

    void Move()
    {
        float hAxis = Input.GetAxis(hInput);
        float vAxis = Input.GetAxis(vInput);

        Vector3 direction = new Vector3(hAxis, 0f, vAxis).normalized;
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
        if (rCastJ.isGrounded && Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpPower, 0) * 50, ForceMode.Acceleration);
            rCastJ.isGrounded = false;
        }
    }
}