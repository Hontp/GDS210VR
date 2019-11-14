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
        int previousSpawnLocactionInt;
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
            int randInt = Random.Range(0, 6);
            while(randInt == previousSpawnLocactionInt && whileLimiter < 10)
            {
                randInt = Random.Range(0, 6);
                whileLimiter++;
            }
            whileLimiter = 0;
            previousSpawnLocactionInt = randInt;
            Transform spawnLoc = spawnLocationHolder.transform.GetChild(randInt);
            GameObject newEnemy = Instantiate(basicEnemy, spawnLoc.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyScript>().GiveInfo(this,playerTransform );
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
