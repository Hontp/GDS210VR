using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiCutter;

namespace SamuraiCutter
{
        
    public class UIManager : MonoBehaviour
    {
        public float impulse;
        public float dampenPerFrame;
        

        private MeshRenderer alert;
        // Start is called before the first frame update
        void Start()
        {
            alert = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if(impulse > 0)
            impulse -= dampenPerFrame;

            alert.material.SetFloat("_alpha",impulse);
            
        }

        public void SetHurt(float amount)
        {
            impulse += amount;
        }
    }

}