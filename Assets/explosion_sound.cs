using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop {
    public class explosion_sound : MonoBehaviour
    {
        public TowerDropSoundController tsc = GameObject.Find("SoundManager").GetComponent<TowerDropSoundController>();
        public Rigidbody rb;
        bool soundPlayed;
        // Start is called before the first frame update
        void Start()
        {
            soundPlayed = false;
            rb = GetComponent<Rigidbody>();
            tsc.playSFX(tsc.GetComponent<AudioSource>(), "0017_explo_bomb_04_PremiumBeat");
        }

        // Update is called once per frame
       
    }
}
