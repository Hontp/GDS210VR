using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace MemeMachine
{
    public class EnemyScript : MonoBehaviour
    {
        int myHealth;
        EnemySpawner mySpawner;
        Transform firstLocation;
        bool movingToPlayer = false;

        [SerializeField]
        NavMeshAgent myAgent;
        [SerializeField]
        Transform playerTransform;
        Animator anim;


        // Start is called before the first frame update
        void Start()
        {
            myHealth = 5;
            anim = GetComponentInChildren<Animator>();

        }

        public void DamageEnemy(int damageTaken)
        {
            myHealth -= damageTaken;
            if(myHealth < 1)
            {
                Destroy(gameObject);
                mySpawner.enemies.Remove(this);
                mySpawner.numOfEnimies--;
            }
        }
        public void GiveInfo(EnemySpawner spawnerToGive, Transform PlayerLoc, Transform intermediatePos)
        {
            mySpawner = spawnerToGive;
            playerTransform = PlayerLoc;
            firstLocation = intermediatePos;
            myAgent.SetDestination(firstLocation.position);
        }

        void ChangeLocation()
        {
            if (myAgent.remainingDistance < 3 && movingToPlayer)
            {
                myAgent.isStopped = true;
                anim.SetBool("Attack", true);
                anim.SetBool("Run", false);

            }
            if (myAgent.remainingDistance < 1 && !movingToPlayer)
            {
                movingToPlayer = true;
                myAgent.SetDestination(playerTransform.position);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }

        }

        private void Update()
        {
            ChangeLocation();
        }


    }
}
