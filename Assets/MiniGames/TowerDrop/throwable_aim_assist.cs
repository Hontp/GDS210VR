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
        if (transform.parent == null)
        {
            if (target == null)
            {

                bool hit = Physics.SphereCast(transform.position, 1, RB.velocity, out rayhit);
                if (hit && rayhit.collider.tag == "Enemy")
                {
                    target = rayhit.collider.gameObject;
                }
            }
            else
            {
                transform.position = transform.position = Vector3.Lerp(transform.position, target.transform.position, ark / 100);
            }
        }
    }
}
