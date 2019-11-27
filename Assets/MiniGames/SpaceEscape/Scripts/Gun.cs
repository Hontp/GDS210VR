using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


namespace MemeMachine
{

    public class Gun : MonoBehaviour
    {
        [SerializeField]




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
            if (grabPinch.GetLastStateDown(inputSource))
            {
                shoot = true;
            }
            if (grabPinch.GetLastStateUp(inputSource))
            {
                shoot = false;
            }

            //if MagazineScript.isLoaded = true;
            if (gunBackGripGrabbed == true)
            {
                if (shoot && TestAmmo())
                {
                    if (Time.time > shootRateTimeStamp)
                    {
                        Shoot();
                        vibration.Execute(0, 0.1f, 300f, 1, inputSource);
                    }
                }
                else if (shoot)
                {
                    //click sound for out of ammo
                }
            }
        }

        public void Shoot()
        {
            Vector3 angleInfo = transform.rotation.eulerAngles + new Vector3(0, 0, 0);
            GameObject shot = Instantiate(bullet, transform.position + transform.forward * 0.55f, Quaternion.Euler(angleInfo.x, angleInfo.y, angleInfo.z));
            shot.GetComponent<Rigidbody>().velocity = transform.forward * 100f;
            shot.GetComponent<Bullet>().DestroyBullet(3f);
            UseAmmo();
        }

        public void UseAmmo(/*put magazine script here as a thing*/)
        {
            /*thing --;*/
        }

        public bool TestAmmo(/*put magazine script here as a thing*/)
        {
            /*
                 if(thing.ammoRemaining < 1)
                 {
                    return false;
                 }
                 else
                 {
                    return true;
                 }            
             */
            return true;
        }

        public void UpdateAmmoText()
        {

        }


    }
  
}
