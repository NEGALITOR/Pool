using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastJump : MonoBehaviour
{
    //Raycast
    public GameObject currentHitObject;
    public float sphereRadius;
    public float maxSphereDist;
    public LayerMask layerMask;
    private Vector3 sphereOrigin;
    private Vector3 sphereDir;
    private float currentHitDistance;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sphereOrigin = transform.position;
        sphereDir = -transform.up;
        RaycastHit hit;

        if (Physics.SphereCast(sphereOrigin, sphereRadius, sphereDir, out hit, maxSphereDist, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = maxSphereDist;
            currentHitObject = null;
        }

        if (currentHitObject != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(sphereOrigin, sphereOrigin + sphereDir * currentHitDistance);
        Gizmos.DrawWireSphere(sphereOrigin + sphereDir * currentHitDistance, sphereRadius);
    }
}