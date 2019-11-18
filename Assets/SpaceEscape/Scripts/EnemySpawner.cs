using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        [SerializeField]
        TMP_Text mainText;
        [SerializeField]
        Slider FuelSlider;



        float enemyTimer;
        GameObject spawnLocationHolder;
        int previousSpawnLocactionInt,
            previousMiddleLocationInt;
        int whileLimiter;
        float timeLeft;

        const float GAMETIME = 180;
        const string FUELSTART = "- Refueling In Process -" + "\n" + "ETC : ";
        const string FUELMIDDLE = " Seconds" + "\n" + "Current Fuel Level : ";
        const string FUELEND = "%";


        // Start is called before the first frame update
        void Start()
        {
            whileLimiter = 0;
            enemyTimer = 0;
            numOfEnimies = 0;
            spawnLocationHolder = gameObject;
            timeLeft = GAMETIME;
        }

        // Update is called once per frame
        void Update()
        {
            EnemySpawnCounter();
            CountDownTimer();
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

        void CountDownTimer()
        {
            float fuelPercentage = (GAMETIME - timeLeft) / GAMETIME * 100;
            timeLeft -= Time.deltaTime;
            mainText.text = FUELSTART + Mathf.RoundToInt(timeLeft).ToString() + FUELMIDDLE + Mathf.RoundToInt(fuelPercentage).ToString() + FUELEND;
            FuelSlider.value = fuelPercentage/100;
        }
    }
}
