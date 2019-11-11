using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemeMachine
{
    public class EnemySpawner : MonoBehaviour
    {
        public int numOfEnimies;
        public List<EnemyScript> enemies;
        
        [SerializeField]
        GameObject basicEnemy;
        [SerializeField]
        Transform playerTransform;

        float enemyTimer;
        GameObject spawnLocationHolder;


        // Start is called before the first frame update
        void Start()
        {
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
            Transform spawnLoc = spawnLocationHolder.transform.GetChild(Random.Range(0, 6));
            GameObject newEnemy = Instantiate(basicEnemy, spawnLoc.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyScript>().GiveInfo(this,playerTransform );
            enemies.Add(newEnemy.GetComponent<EnemyScript>());
            numOfEnimies++;
        }

        void EnemySpawnCounter()
        {
            if(enemyTimer > 1)
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
