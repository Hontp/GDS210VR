using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using TMPro;


namespace MemeMachine
{

    public class Gun : MonoBehaviour
    {
        [SerializeField]
        TMP_Text ammoTB;



        public SteamVR_Input_Sources rightInputSource;
        public SteamVR_Input_Sources leftInputSource;
        public SteamVR_Action_Boolean grabPinch;
        public SteamVR_Action_Vibration vibration;
        public bool shoot;
        public GameObject bullet;
        public  static bool gunBackGripGrabbed = false;

        public float shootRateTimeStamp;
        public float shootRate = 0.1f;
        //public Transform handPos;

        public Hand rightHand;
        
        public static GameObject currentMag;
       


        public void Start()
        {

            rightHand = GameObject.Find("RightHand").gameObject.GetComponent<Hand>();

        }
        private void Update()
        {
            CheckShoot();

            if(MagazineScript.isLoaded == true)
            {
        
            }
            
            //attachment off set check
            //when item is picked up appply transform 
           if(this.transform.parent == rightHand)
            {
                rightHand.renderModelPrefab = this.gameObject;
            }

            if (grabPinch.GetStateDown(leftInputSource)) //|| right side button press)
            {
                //add throwable and rb on mag
                
                
            }

            UpdateAmmoText();
        }
        private void CheckShoot()
        {
            if (grabPinch.GetLastStateDown(rightInputSource))
            {
                shoot = true;
            }
            if (grabPinch.GetLastStateUp(rightInputSource))
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
                        vibration.Execute(0, 0.1f, 300f, 1, rightInputSource);
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
            currentMag.GetComponent<MagazineScript>().ammoCount -= 1;
        }

        public bool TestAmmo(/*put magazine script here as a thing*/)
        {
            if (currentMag.GetComponent<MagazineScript>().ammoCount < 1)
            {
               return false;
            }
            else
            {
                return true;
            }
            
        }

        public void UpdateAmmoText()
        {
            if (MagazineScript.isLoaded)
            {
                ammoTB.text = currentMag.GetComponent<MagazineScript>().ammoCount.ToString() + " / " + 30;
            }
        }


    }
  
}
