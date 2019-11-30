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



        public SteamVR_Input_Sources inputSource;
        public SteamVR_Action_Boolean grabPinch;
        public SteamVR_Action_Vibration vibration;
        public bool shoot;
        public GameObject bullet;
        public  static bool gunBackGripGrabbed = false;

        public float shootRateTimeStamp;
        public float shootRate;
        //public Transform handPos;

        public Hand rightHand;
        
        public GameObject currentMag = null;
        public GameObject parent;
        public EnemySpawner spawner;

        




        public void Start()
        {

            rightHand = GameObject.Find("RightHand").gameObject.GetComponent<Hand>();
            spawner = FindObjectOfType<EnemySpawner>();
        }
        private void Update()
        {
            CheckShoot();

            if(gunBackGripGrabbed == true)
            {
                //transform.parent.position = Vector3.zero;
            }
            
            //attachment off set check
            //when item is picked up appply transform 
           if(this.transform.parent == rightHand)
            {
                //rightHand.renderModelPrefab = this.gameObject;
               // rightHand.SetRenderModel(this.gameObject);
            }

            //if (grabPinch.GetStateDown(leftInputSource)) //|| right side button press)
            {
                //add throwable and rb on mag
                
                
            }

            UpdateAmmoText();
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
                if (shoot && TestAmmo() && MagazineScript.isLoaded)
                {   
                    if (shootRateTimeStamp < 0)
                    {
                        Shoot();
                    }
                    else
                    {
                        shootRateTimeStamp -= Time.deltaTime;
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
            shootRateTimeStamp = Time.time + shootRate;
            UseAmmo();
            shootRateTimeStamp = shootRate;
            vibration.Execute(0, 0.1f, 300f, 1, inputSource);
            spawner.bulletsShot++;
        }

        public void UseAmmo()
        {
            UpdateAmmoText();
            currentMag.GetComponent<MagazineScript>().ammoCount -= 1;
        }

        public bool TestAmmo()
        {
            UpdateAmmoText();
            if (currentMag.GetComponent<MagazineScript>().ammoCount < 1)
            {
                Debug.Log("out");
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
            else
            {
                ammoTB.text = "0 / 0";
            }
        }


    }
  
}
