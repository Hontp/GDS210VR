using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class woosh : MonoBehaviour
    {
        public TowerDropSoundController tsc= GameObject.Find("SoundManager").GetComponent<TowerDropSoundController>();
        public Rigidbody rb;
        bool soundPlayed;
        // Start is called before the first frame update
        void Start()
        {
            soundPlayed = false;
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (rb.velocity.magnitude > 1 && soundPlayed==false)
            {
                soundPlayed = true;
                tsc.playSFX(tsc.GetComponent<AudioSource>(), "woosh");
            }
        }
    }
}
