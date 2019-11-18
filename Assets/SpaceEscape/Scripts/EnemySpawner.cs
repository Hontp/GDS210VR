using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemeMachine
{
    public class EnemySpawner : MonoBehaviour
    {
        public float SpawnTime;
        public int numOfEnimies;
        public List<EnemyScript> enemies;

        [SerializeField]
        GameObject secondaryLocations;
        [SerializeField]
        GameObject basicEnemy;
        [SerializeField]
        Transform playerTransform;

        float enemyTimer;
        GameObject spawnLocationHolder;
        int previousSpawnLocactionInt,
            previousMiddleLocationInt;
        int whileLimiter;


        // Start is called before the first frame update
        void Start()
        {
            whileLimiter = 0;
            enemyTimer = 0;
            numOfEnimies = 0;
            spawnLocationHolder = gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            EnemySpawnCounter();
        }



        void SpawnEnemies()
        {
            //Code finds a spawn locations and runs a while loop 10 times to try and get it to not spawn at the same location with in reason, if it does the same spawn past that i do not want iot to keep whiling for perfomance reasons
            int randIntSpawnLocation = Random.Range(0, 6);
            while(randIntSpawnLocation == previousSpawnLocactionInt && whileLimiter < 10)
            {
                randIntSpawnLocation = Random.Range(0, 6);
                whileLimiter++;
            }
            whileLimiter = 0;
            previousSpawnLocactionInt = randIntSpawnLocation;


            //finds a middle location in an identical method as the spawn finding location
            int randIntMiddleLocation = Random.Range(0, 6);
            while (randIntMiddleLocation == previousMiddleLocationInt && whileLimiter < 10)
            {
                randIntMiddleLocation = Random.Range(0, 6);
                whileLimiter++;
            }
            whileLimiter = 0;
            previousMiddleLocationInt = randIntMiddleLocation;




            Transform spawnLoc = spawnLocationHolder.transform.GetChild(randIntSpawnLocation);
            GameObject newEnemy = Instantiate(basicEnemy, spawnLoc.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyScript>().GiveInfo(this,playerTransform, secondaryLocations.transform.GetChild(randIntMiddleLocation));
            enemies.Add(newEnemy.GetComponent<EnemyScript>());
            numOfEnimies++;
        }   

        void EnemySpawnCounter()
        {
            if(enemyTimer > (SpawnTime + numOfEnimies/30))
            {
                enemyTimer = 0;
                SpawnEnemies();
            }
            else
            {
                enemyTimer += Time.deltaTime;
            }
        }
    }
}
