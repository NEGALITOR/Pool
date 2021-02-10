using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastForward : MonoBehaviour
{
    public GameObject currentHitObject;
    public float sphereRadius;
    public float maxRaycastDist;
    public LayerMask layerMask;
    private Vector3 sphereOrigin;
    private Vector3 sphereDir;
    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sphereOrigin = transform.position;
        sphereDir = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(sphereOrigin, sphereDir, out hit, maxRaycastDist, layerMask, QueryTriggerInteraction.UseGlobal)) //Physics.SphereCast(sphereOrigin, sphereRadius, sphereDir, out hit, maxSphereDist, layerMask, QueryTriggerInteraction.UseGlobal)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = maxRaycastDist;
            currentHitObject = null;
        }

    }
}