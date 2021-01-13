using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Rigidbody rb;

    public float pSpeed = 10f;
    public float jumpPower = 7f;
    public bool isGrounded = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        Move();

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal2");
        float vAxis = Input.GetAxis("Vertical2");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis);
        rb.AddForce(movement * pSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpPower, 0) * 50, ForceMode.Acceleration);
            isGrounded = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}

