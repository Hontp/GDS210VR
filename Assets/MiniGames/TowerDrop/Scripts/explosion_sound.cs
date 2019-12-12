using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop {
    public class explosion_sound : MonoBehaviour
    {
        public TowerDropSoundController tsc;
        public Rigidbody rb;
        bool soundPlayed;
        // Start is called before the first frame update
        void Start()
        {
           tsc = GameObject.Find("SoundManager").GetComponent<TowerDropSoundController>();
          
            soundPlayed = false;
            rb = GetComponent<Rigidbody>();
            tsc.playSFX(transform.GetComponent<AudioSource>(), "0017_explo_bomb_04");
        }

        // Update is called once per frame

    }
}
