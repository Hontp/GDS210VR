using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

namespace MemeMachine
{
    public class SciGrenade : MonoBehaviour
    {
       
        public bool grenadeTick = false;
        public float detTime = 2f;
        public GameObject grenade;
        public bool isGrabbed = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (grenade.GetComponent<Throwable>().attached == true)
            {
                isGrabbed = true;
            }
            if (grenade.GetComponent<Throwable>().attached == false && isGrabbed == true)
            {
                StartExplosion();
            }
        }
        /* private void OnTriggerEnter(Collider other)
         {
             if(gameObject.tag == "ground")
             {
                 StartExplosion();
             }
         }*/

        void StartExplosion()
        {
            StartCoroutine("GrenadeWaitTime");
            if (grenadeTick == true)
            {
                RaycastHit[] enemiesHit;
                enemiesHit = Physics.SphereCastAll(this.transform.position, 10f, Vector3.forward);
                for (int i = 0; i < enemiesHit.Length; i++)
                {
                    Debug.Log(enemiesHit[i].collider.gameObject.name);
                    //Destroy(enemiesHit[i].collider.gameObject);
                }
            }
            grenadeTick = false;
        }

        IEnumerator GrenadeWaitTime()
        {
            yield return new WaitForSeconds(detTime);
            grenadeTick = true;
        }
    }
}
