using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace MemeMachine
{

    public class Gun : MonoBehaviour
    {
        public SteamVR_Input_Sources inputSource;
        public SteamVR_Action_Boolean grabPinch;
        public SteamVR_Action_Vibration vibration;
        public bool shoot;
        public GameObject bullet;
        public bool gunBackGripGrabbed = false;

        public float shootRateTimeStamp;
        public float shootRate = 0.1f;
        public void Shoot()
        {
            Vector3 angleInfo = transform.rotation.eulerAngles + new Vector3(0, 0, 0);
            GameObject shot = Instantiate(bullet, transform.position + transform.forward * 0.55f, Quaternion.Euler(angleInfo.x, angleInfo.y, angleInfo.z));
            shot.GetComponent<Rigidbody>().velocity = transform.forward * 100f;
            shot.GetComponent<Bullet>().DestroyBullet(3f);
            shootRateTimeStamp = Time.time + shootRate;

          

        }
        private void Update()
        {
            if (this.GetComponent<Throwable>().attached == true)
            {
                gunBackGripGrabbed = true;
                this.GetComponent<Throwable>().enabled = false;
            }
             
        if(gunBackGripGrabbed == true)
            {
                CheckShoot();
            }

           
        }
        private void CheckShoot()
        {
            if (grabPinch.GetStateDown(inputSource))
            {
                if (Time.time > shootRateTimeStamp)
                {
                    Shoot();
                    shoot = false;
                    vibration.Execute(0, 0.3f, 300f, 1, inputSource);
                }
  
            }

        }
       
    }

}
