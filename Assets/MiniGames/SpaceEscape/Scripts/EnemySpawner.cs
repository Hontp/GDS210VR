using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace MemeMachine
{
    public class EnemySpawner : MonoBehaviour
    {
        public MenuSystem menu;
        public bool gameUnderway;
        public float SpawnTime;
        public int numOfEnimies;
        public List<EnemyScript> enemies;
        public float spawnLimiter;
        public float spawnDecrease;
        public int bulletsShot;
        public int score;
        public int difficulty;

        public AudioClip backgroundMusic;
        public AudioSource audioSource;

        public void SetSpawnVariables(float limit, float decrease, float time, int dif)
        {
            SpawnTime = time;
            spawnLimiter = limit;
            spawnDecrease = decrease;
            difficulty = dif;
        }

        [SerializeField]
        GameObject secondaryLocations;
        [SerializeField]
        GameObject Enemy;
        [SerializeField]
        Transform playerTransform;
        [SerializeField]
        PlayerScript playerScript;
        [SerializeField]
        GameObject WinScreenPrefab;

        [SerializeField]
        TMP_Text mainText;
        [SerializeField]
        Slider FuelSlider;
        [SerializeField]
        GameObject rightHandObject;
        [SerializeField]
        Hand rightHand;



        float enemyTimer;
        GameObject spawnLocationHolder;
        int previousSpawnLocactionInt,
            previousMiddleLocationInt;
        int whileLimiter;
        float timeLeft;
        ItemReseter[] items;

        const float GAMETIME = 180;
        const string FUELSTART = "- Refueling In Process -" + "\n" + "ETC : ";
        const string FUELMIDDLE = " Seconds" + "\n" + "Current Fuel Level : ";
        const string FUELEND = "%";



        // Start is called before the first frame update
        void Start()
        {
            items = FindObjectsOfType<ItemReseter>();
            print(items.Length + " items in scene");
            gameUnderway = false;
            menu = FindObjectOfType<MenuSystem>();
            playerScript.menu = menu;
            whileLimiter = 0;
            enemyTimer = 0;
            numOfEnimies = 0;
            spawnLocationHolder = gameObject;
            timeLeft = GAMETIME;

            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
           // audioSource.Play();
        }

        // Update is called once per frame
        void Update()
        {
            if(menu.gamePlaying & !gameUnderway)
            {
                gameUnderway = true;
                playerScript.SetPlayerHealth(6);
                FindObjectOfType<rightHand>().GameStarted();
                //reset all mags position and ammo


            }
            if (menu.gamePlaying & gameUnderway)
            {
                EnemySpawnCounter();
                CountDownTimer();
            }
            TestWin();
        }


        #region Enemies
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
            GameObject newEnemy = Instantiate(Enemy, spawnLoc.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyScript>().GiveInfo(this,playerTransform, secondaryLocations.transform.GetChild(randIntMiddleLocation), playerScript);
            enemies.Add(newEnemy.GetComponent<EnemyScript>());
            numOfEnimies++;
        }   

        void EnemySpawnCounter()
        {
            if(enemyTimer > (SpawnTime + numOfEnimies/75))
            {
                enemyTimer = 0;
                SpawnEnemies();
                if(SpawnTime > 0.2)
                {
                    SpawnTime -= 0.07f;
                }
            }
            else
            {
                enemyTimer += Time.deltaTime;
            }
        }
        #endregion


        void TestWin()
        {
            if(timeLeft < 0 && playerScript.GetHealth() > 0)
            {
                GameFinished();
                GameObject WinScreen = Instantiate<GameObject>(WinScreenPrefab);
                WinScreen.GetComponentInChildren<TMP_Text>().text = "you got a score of " + score.ToString(); ;
                menu.Invoke("MenuActive", 4);
                FindObjectOfType<rightHand>().MenuReset();
                Destroy(WinScreen, 4);
            }
        }

        void CountDownTimer()
        {
            float fuelPercentage = (GAMETIME - timeLeft) / GAMETIME * 100;
            timeLeft -= Time.deltaTime;
            mainText.text = FUELSTART + Mathf.RoundToInt(timeLeft).ToString() + FUELMIDDLE + Mathf.RoundToInt(fuelPercentage).ToString() + FUELEND;
            FuelSlider.value = fuelPercentage/100;
        }

        public void GameFinished()
        {
            for(int ii = 0; ii < items.Length; ii++)
            {
                items[ii].ResetItems();
            }
            menu.gamePlaying = false;
            gameUnderway = false;
            whileLimiter = 0;
            enemyTimer = 0;
            numOfEnimies = 0;
            timeLeft = 0;
            for(int ii = 0; ii < enemies.Count; ii++)
            {
                Destroy(enemies[ii].gameObject);
            }
            enemies.Clear();
            score = (int)((GAMETIME - timeLeft) * (8 + difficulty * 2) - bulletsShot * (difficulty / 2));
            /*                                  *\

              Lincoln put score saving code here
              
            \*                                  */
            PlayerPrefs.SetFloat("spaceEscapeScore", score);

            if(PlayerPrefs.GetFloat("spaceEscapeHiScore") < score)
            {
                PlayerPrefs.SetFloat("spaceEscapeHiScore", score);
            }
            timeLeft = GAMETIME;            
        }

    
    }
}
