﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

namespace MemeMachine
{
    public class PlayerScript : MonoBehaviour
    { 
        public MenuSystem menu;

        [SerializeField]
        EnemySpawner spawner;
        [SerializeField]
        GameObject deathScreenPrefab;
        [SerializeField]
        TMP_Text healthTB;
        [SerializeField]
        RawImage healthImage;
        [SerializeField]
        Color orange;

        int playerHealth = 1000;
        public AudioSource audioSource;
        public AudioClip music;



        public void Start()
        {
            if (menu.gamePlaying)
            {
                audioSource.clip = music;
                audioSource.loop = true;
                audioSource.Play();

            }
            if(!menu.gamePlaying && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            print(audioSource.isPlaying);
            UpdatePlayerHealthDisplay();
        }

        private void Update()
        {
            if (menu.gamePlaying)
            {
                audioSource.clip = music;
                audioSource.loop = true;
                audioSource.Play();

            }
            if (!menu.gamePlaying && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        public void DamagePlayer()
        {
            playerHealth -= 1;
            UpdatePlayerHealthDisplay();
            TestPlayerDeath();
        }

        public void SetPlayerHealth(int health)
        {
            playerHealth = health;
            UpdatePlayerHealthDisplay();
        }

        public void TestPlayerDeath()
        {
            if(playerHealth < 1)
            {
                print("player has died");
                spawner.GameFinished();
                GameObject deathScreen = Instantiate<GameObject>(deathScreenPrefab);
                deathScreen.GetComponentInChildren<TMP_Text>().text = "You died noob with a score of " + spawner.score;
                menu.Invoke("MenuActive", 4);
                FindObjectOfType<rightHand>().MenuReset();
                Destroy(deathScreen, 4);

            }
        }
        public int GetHealth()
        {
            return playerHealth;
        }

        public void UpdatePlayerHealthDisplay()
        {
            healthTB.text = playerHealth.ToString();
            if(playerHealth > 4)
            {
                healthImage.color = Color.green;
            }
            else if (playerHealth > 2)
            {
                healthImage.color = Color.yellow;
            }
            else if (playerHealth > 0)
            {
                healthImage.color = orange;
            }
            else if (playerHealth < 1)
            {
                healthImage.color = Color.red;
            }
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
