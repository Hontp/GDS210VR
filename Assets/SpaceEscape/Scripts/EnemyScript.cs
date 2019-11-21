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

        [SerializeField]
        NavMeshAgent myAgent;
        [SerializeField]
        Transform playerTransform;


        // Start is called before the first frame update
        void Start()
        {
            myHealth = 5;
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
            if(myAgent.remainingDistance < 1)
            {
                myAgent.SetDestination(playerTransform.position);
            }
        }

        private void Update()
        {
            ChangeLocation();
        }


    }
}
