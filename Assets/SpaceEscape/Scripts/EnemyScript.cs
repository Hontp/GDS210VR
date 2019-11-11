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

        [SerializeField]
        NavMeshAgent myAgent;
        [SerializeField]
        Transform playerTransform;


        // Start is called before the first frame update
        void Start()
        {
            //myAgent.SetDestination(playerTransform.position);
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
        public void GiveInfo(EnemySpawner spawnerToGive, Transform PlayerLoc)
        {
            mySpawner = spawnerToGive;
            playerTransform = PlayerLoc;
            myAgent.SetDestination(playerTransform.position);
        }



    }
}
