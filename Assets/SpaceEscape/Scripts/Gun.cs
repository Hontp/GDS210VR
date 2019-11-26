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
        public  static bool gunBackGripGrabbed = false;

        public float shootRateTimeStamp;
        public float shootRate = 0.1f;
        //public Transform handPos;

        public Hand rightHand;

        public void Start()
        {

            rightHand = GameObject.Find("RightHand").gameObject.GetComponent<Hand>();

        }
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
            CheckShoot();

            if(gunBackGripGrabbed == true)
            {

            }
            
            //attachment off set check
            //when item is picked up appply transform 
           if(this.transform.parent == rightHand)
            {
                rightHand.renderModelPrefab = this.gameObject;
            }
          
        
        }
        private void CheckShoot()
        {
            //if MagazineScript.isLoaded = true;
            if(gunBackGripGrabbed == true)
            {
                if (grabPinch.GetLastStateDown(inputSource))
                {
                    if (Time.time > shootRateTimeStamp)
                    {
                        Shoot();
                        vibration.Execute(0, 0.3f, 300f, 1, inputSource);
                    }

                }
            }
         

        }
       
    }
  
}
