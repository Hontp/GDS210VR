using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SamuraiCutter;

namespace SamuraiCutter
{

    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        private Transform myTransform;
        private NavMeshAgent nma;
        public float rotationSpeed = 3,
                    MoveSpeed = 4,
                    MinDist = 5,
                    MaxDist = 10f;

        private SpawnEnemy spawnEnemy;
        private Rigidbody rb;
        public bool jumping;
        public bool attacking;

        public enum State {IDLE,WALK,JUMP,};

        public Animator animator;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = MoveSpeed;
            nma.SetDestination(GameManager._instance.playerPos.position);
            spawnEnemy = GameManager._instance.spawnEnemy;
        }

        // Update is called once per frame
        void Update()
        {
            if(Vector3.Distance(GameManager._instance.playerPos.position, transform.position) < 1.6f && !jumping)
            {
                //jumping = true;
                nma.isStopped = true;
                
                //jumpBack();
                slash();
            }
            else
            {
                attacking = false;
            }
            if(animator != null)
            animation();
            //Movement();
        }

        void animation()
        {
            if(jumping && !attacking)
            {
                animator.SetBool("flip",true);
                animator.SetBool("attack",false);
            }

            if(attacking && !jumping)
            {
                animator.SetBool("flip", false);
                animator.SetBool("attack",true);
                //attacking = false;
            }

            if(!attacking && !jumping)
            {
                nma.isStopped = false;
            }
        }

        public void slash()
        {
            attacking = true;
        }

        public void jumpBack()
        {
            
            nma.enabled = false;
            //new Vector3( (((transform.position-GameManager._instance.playerPos.position).normalized).x), -1.0f, (((transform.position-GameManager._instance.playerPos.position).normalized).z))
            rb.AddForce( new Vector3( (((transform.position-GameManager._instance.playerPos.position).normalized).x), 5f, (((transform.position-GameManager._instance.playerPos.position).normalized).z)), ForceMode.VelocityChange);
            //rb.constraints = RigidbodyConstraints.FreezeRotationY & RigidbodyConstraints.FreezeRotationZ & RigidbodyConstraints.FreezePosition;
            //rb.AddTorque(transform.right * -180f);
        }

        public void OnCollisionEnter(Collision col)
        {
            if(col.transform.CompareTag("floor"))
            {
                jumping = false;
                nma.enabled = true;
                nma.isStopped = false;
                nma.speed = MoveSpeed;
            nma.SetDestination(GameManager._instance.playerPos.position);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            animator.SetBool("flip", false);
                animator.SetBool("attack",false);
            }
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

}