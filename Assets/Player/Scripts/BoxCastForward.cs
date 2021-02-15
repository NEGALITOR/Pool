using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastForward : MonoBehaviour
{
    public GameObject currentHitObject;
    private Vector3 boxOrigin;
    private Vector3 boxDir;
    private float currentHitDistance;
    public Vector3 boxHeight;
    public Vector3 boxRadius;
    public float maxDistance;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boxOrigin = transform.position - boxHeight;
        boxDir = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.BoxCast(boxOrigin, boxRadius, boxDir, out hit, transform.rotation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {

            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        }
        else
        {

            currentHitDistance = maxDistance;
            currentHitObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(boxOrigin, transform.TransformDirection(Vector3.forward) * currentHitDistance);
        Gizmos.DrawWireCube(boxOrigin + transform.forward * currentHitDistance, boxRadius);
        
    }
}
