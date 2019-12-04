using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace MemeMachine
{
    public class EnemyScript : MonoBehaviour
    {

        [SerializeField]
        NavMeshAgent myAgent;
        [SerializeField]
        Transform playerTransform;

        Animator anim;
        EnemySpawner mySpawner;
        Transform firstLocation;
        PlayerScript playerScript;
        bool movingToPlayer = false;
        bool attackingPlayer = false;
        bool attacksStarted = false;
        float attackCounter = 0;
        int myHealth;


        // Start is called before the first frame update
        void Start()
        {
            myHealth = 5;
            anim = GetComponentInChildren<Animator>();
        }

        public void DamageEnemy(int damageTaken)
        {
            myHealth -= damageTaken;
            print(myHealth);
            if(myHealth < 1)
            {
                myAgent.isStopped = true;
                anim.SetBool("Die", true);
                anim.SetBool("Attack", false);
                anim.SetBool("Run", false);

                Destroy(gameObject, 2f);
                mySpawner.enemies.Remove(this);
                mySpawner.numOfEnimies--;
            }
        }
        public void GiveInfo(EnemySpawner spawnerToGive, Transform PlayerLoc, Transform intermediatePos, PlayerScript player)
        {
            mySpawner = spawnerToGive;
            playerTransform = PlayerLoc;
            firstLocation = intermediatePos;
            myAgent.SetDestination(firstLocation.position);
            playerScript = player;
        }

        void ChangeLocation()
        {
            if ( (playerTransform.position - transform.position).magnitude < 3)
            {
                myAgent.isStopped = true;
                anim.SetBool("Attack", true);
                anim.SetBool("Run", false);
                anim.SetBool("Die", false);
                attackingPlayer = true;
            }
            if (myAgent.remainingDistance < 1 && !movingToPlayer)
            {
                movingToPlayer = true;
                myAgent.SetDestination(playerTransform.position);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
                anim.SetBool("Die", false);
            }
        }

        void Attack()
        {
            if (attackingPlayer && !attacksStarted)
            {
                attacksStarted = true;
                print("attacking player");
                attackCounter = 1;
            }
            if(attacksStarted && attackCounter < 0)
            {
                playerScript.Invoke("DamagePlayer", 0);
                attackCounter = 1;
            }
            else
            {
                attackCounter -= Time.deltaTime;
            }
        }


        private void Update()
        {
            ChangeLocation();
            Attack();
        }


    }
}
