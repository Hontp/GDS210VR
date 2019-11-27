using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR.InteractionSystem;

namespace MemeMachine
{
    public class PlayerScript : MonoBehaviour
    { 
        public MenuSystem menu;

        [SerializeField]
        EnemySpawner spawner;
        [SerializeField]
        GameObject deathScreenPrefab;

        public GameObject laserPointer;
        int playerHealth = 6;
        public Hand rightHand;



        public void Start()
        {
            rightHand = GameObject.Find("RightHand").gameObject.GetComponent<Hand>();

        }

        public void DamagePlayer()
        {
            playerHealth -= 1;
            TestPlayerDeath();
        }

        public void SetPlayerHealth(int health)
        {
            playerHealth = health;
        }

        public void TestPlayerDeath()
        {
            if(playerHealth < 1)
            {
                print("player has died");
                spawner.GameFinished();
                GameObject deathScreen = Instantiate<GameObject>(deathScreenPrefab);
                deathScreen.GetComponentInChildren<TMP_Text>().text = "You died noob";
                menu.Invoke("MenuActive", 4);
                Destroy(deathScreen, 4);
                //player dead
                //rightHand.renderModelPrefab = laserPointer;

            }
        }
        public int GetHealth()
        {
            return playerHealth;
        }








        private void OnTriggerEnter(Collider objectEntering)
        {
            if (objectEntering != null && objectEntering.CompareTag("Enemy"))
            {
                //objectEntering.gameObject.GetComponent<EnemyScript>().DamageEnemy(10000);
            }
        }
    }

}
