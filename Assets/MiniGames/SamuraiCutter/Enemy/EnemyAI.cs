using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SamuraiCutter;
using System;

namespace SamuraiCutter
{

    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        private Transform myTransform;
        private NavMeshAgent nma;
        public float rotationSpeed = 3,
                    MoveSpeed = 7,
                    MinDist = 5,
                    MaxDist = 10f;

        private SpawnEnemy spawnEnemy;
        private Rigidbody rb;
        public bool jumping;
        public bool attacking;
        public AudioSource playerHit;
        public bool idle;
        public float AttackSpeed = 1.25f;
        public enum State {IDLE,WALK,JUMP};

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
            if(Vector3.Distance(GameManager._instance.playerPos.position, transform.position) < 2.5f && !jumping && !attacking)
            {
                idle = true;
                //jumping = true;
                
                if(nma.isActiveAndEnabled)
                {
                    nma.speed = 0;
                    nma.isStopped = true;
                }
                
                //var rand = Random.Range(0,1000);
                //if(rand > 0 && rand <= 700)
                //{
                //    slash();
                //}

                if(AttackSpeed >= 0)
                {
                    AttackSpeed -= Time.deltaTime;
                    slash();
                }
                else
                {
                    Debug.Log("Attack The Worm!");
                    AttackSpeed = 1.25f;
                    slash();
                    
                }
         
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

            if(!attacking && !jumping && !idle)
            {
                
                nma.speed = MoveSpeed;
                if (nma.isOnNavMesh)
                {
                    nma.isStopped = false;
                }

            }
            if (jumping && !attacking)
            {
                animator.SetBool("flip", true);
                animator.SetBool("attack", false);
                if (nma.isOnNavMesh)
                {
                    nma.isStopped = false;
                }
                jumping = false;
                nma.speed = 0;
            }

            if (attacking && !jumping)
            {
                animator.SetBool("flip", false);
                animator.SetBool("attack",true);

                if (nma.isOnNavMesh)
                {
                    nma.isStopped = false;
                }
                
                nma.speed = 0;
                attacking = false;
            }
            else if(idle)
            {
                animator.SetBool("idle", true);
                animator.SetBool("attack",false);

                if (nma.isOnNavMesh)
                {
                    nma.isStopped = false;
                }
                nma.speed = 0;
                attacking = false;
            }

            
        }

        public void slash()
        {
            attacking = true;

        }

        public void jumpBack()
        {
            // does the animation
           jumping = true;
           //new Vector3( (((transform.position-GameManager._instance.playerPos.position).normalized).x), -1.0f, (((transform.position-GameManager._instance.playerPos.position).normalized).z))
            Invoke("BackJumpForce",0.5f);
        }

        public void BackJumpForce()
        {
             rb.isKinematic = false;
           rb.useGravity = true;
           rb.AddForce( new Vector3( 3f*(((transform.position-GameManager._instance.playerPos.position).normalized).x), 2f, 3f*(((transform.position-GameManager._instance.playerPos.position).normalized).z)), ForceMode.VelocityChange);
           rb.constraints = RigidbodyConstraints.FreezeRotationX & RigidbodyConstraints.FreezeRotationY & RigidbodyConstraints.FreezeRotationY;
            //rb.AddTorque(transform.right * -180f);

            if (nma.isOnNavMesh)
            {
                nma.isStopped = true;
            }
           nma.enabled = false;
           idle = false;
        }

        public void OnCollisionEnter(Collision col)
        {
            if(col.transform.CompareTag("floor"))
            {
                try
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    jumping = false;
                    nma.enabled = true;
                    nma.isStopped = false;
                    nma.speed = MoveSpeed;
                    nma.SetDestination(GameManager._instance.playerPos.position);
                    animator.SetBool("flip", false);
                    animator.SetBool("attack", false);
                    idle = false;
                }
                catch (Exception e)
                {

                }
                
            }
        }

       
    }

}