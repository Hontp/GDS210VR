using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private Transform myTransform;

    public float rotationSpeed = 3,
                 MoveSpeed = 4,
                 MinDist = 5,
                 MaxDist = 10f;

    private void Awake()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {

        #region SimpleMethod
        float distance = Vector3.Distance(transform.position, target.position);

        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        if (distance >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (distance <= MaxDist)
            {
                //run attack trigger here
            }
        }
        Debug.Log(distance);
        #endregion


        #region Testing

        #region Simple No If Statments
        ///* Rotation - Look At Player */
        //myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        ///* Movement - Move Towards Player */
        //myTransform.position += transform.forward * MoveSpeed * Time.deltaTime;
        #endregion

        #region Issues RigidBody
        /* Rigidbody component will need to be referenced */

        //float distance = Vector3.Distance(transform.position, target.position);

        //if (distance >= MinDist)
        //{
        //    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        //    rb.AddForce(myTransform.forward * MoveSpeed * Time.deltaTime);

        //    if (distance < MinDist)
        //    {
        //        //attack                
        //    }
        //}
        #endregion

        #endregion

    }
}
