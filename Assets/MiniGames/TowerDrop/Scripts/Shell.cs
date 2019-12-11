using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class Shell : MonoBehaviour
    {
        public float MaxDamage = 5.5f;
        public float MaxLifeTime = 5.5f;


        public TowerDropSoundController sfxController;
        private AudioSource sfxSource;

        private void Start()
        {            
            Destroy(gameObject, MaxLifeTime);
            sfxSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerHealth>().hit = true;

                if (sfxController != null)
                    sfxController.playSFX(sfxSource, "0017_explo_bomb_02");

                Destroy(gameObject);
            }

            Destroy(gameObject, MaxLifeTime);
        }   
    }
}