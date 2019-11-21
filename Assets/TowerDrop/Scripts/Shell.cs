using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float shellSpeed = 5;


    public void SetTarget( Transform targetTransform)
    {
        target = targetTransform;
    }

    public void SetShellSpeed( float speed)
    {
        shellSpeed = speed;
    }

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, shellSpeed * Time.deltaTime);
        }
    }
}
