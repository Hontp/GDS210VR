using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiCutter
{

    public class SpawnEnemy : MonoBehaviour
    {
        public Transform enemySpawn1, enemySpawn2, enemySpawn3;
        public GameObject respawnEnemy;
        // I believe in this curve
        public AnimationCurve waveCurve;
        public int currentWaveNumber =0;
        [SerializeField]
        [Header("Max Wave Number (Here for reference)")]
        public const int maxWaves = 20;
        public int[] enemyAmounts = new int[maxWaves];
        public bool spawning;
        public int waveTimeLength;

        public int enemiesSpawned;
        public int remainingEnemies;

        public float waveStartTime;

        public float sectionTime = 1f;

        public AnimationCurve waveSpawnCurve;

        public void registerKill()
        {
            remainingEnemies--;
        }

        public void incWave()
        {
            currentWaveNumber++;
            waveStartTime = Time.time;
            remainingEnemies = enemyAmounts[currentWaveNumber];
            enemiesSpawned = 0;
            
        }

        public int sampleWave(int waveNumber)
        {
            return (int)( waveCurve.Evaluate(0.05f * waveNumber) * 20f);
        }

        public int sampleSpawns(float timeSinceWaveStart)
        {
            return (int) (waveSpawnCurve.Evaluate(0.05f * timeSinceWaveStart) * waveTimeLength);
        }


        void Start()
        {
            for(int i=0;i<maxWaves;i++)
            {
                enemyAmounts[i] = sampleWave(i);
            }
            waveStartTime = Time.time;
        }


        public void Update()
        {
            // TODO: Gradually spawn in the enemies
            if(enemiesSpawned < enemyAmounts[currentWaveNumber] && !spawning)
            {
                Debug.Log("spawning Active");
                spawning = true;
                Invoke("Spawn", 1f);
            }
            else
            {
                if(remainingEnemies == 0)
                {
                    incWave();
                }
            }
        }

        public void Spawn()
        {
            if (spawning)
            {
                int spawn = Random.Range(0, 3);
                Transform currentTransform;
                switch (spawn)
                {
                    case 0:
                        currentTransform = enemySpawn1;
                        break;
                    case 1:
                        currentTransform = enemySpawn2;
                        break;

                    case 2:
                        currentTransform = enemySpawn3;
                        break;
                    default:
                        currentTransform = enemySpawn1;
                        break;
                }
                Instantiate(respawnEnemy, currentTransform.position, currentTransform.rotation);
                enemiesSpawned++;
                spawning = false;
                Debug.Log("spawning complete");
            }
        }
    }
}