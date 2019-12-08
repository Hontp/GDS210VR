using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwable_aim_assist : MonoBehaviour
{
    public GameObject target;
    public float ark;

    public Ray ray;
    public RaycastHit rayhit;

    Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (target == null)
        {
            
            bool hit =Physics.SphereCast(transform.position,10, RB.velocity, out rayhit);
            if (hit)
            {
                target=rayhit.collider.gameObject;
            }
        }
        else
        {
            transform.position = transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.01f);
        }
    }
}
