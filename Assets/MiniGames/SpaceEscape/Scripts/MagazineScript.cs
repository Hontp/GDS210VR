using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace MemeMachine
{
    public class MagazineScript : MonoBehaviour
    {
        public const int STARTINGAMMOCOUNT = 30;

        public Hand hand;
        public GameObject x;
        public GameObject blaster;
        public static bool isLoaded = false;

        public Rigidbody rb;
        bool attached;

        public int ammoCount = 30;
        public bool hasAmmo = true;

        public bool unlimited_ammo=false;


        // Start is called before the first frame update
        void Start()
        {
            ammoCount = STARTINGAMMOCOUNT;
            hand = GameObject.Find("LeftHand").gameObject.GetComponent<Hand>();
            print(blaster);
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.parent == null && rb.isKinematic == true)
            {
                rb.isKinematic = false;
                FindObjectOfType<Gun>().currentMag = null;
                isLoaded = false;
                FindObjectOfType<Gun>().UpdateAmmoText();
                GetComponent<MeshRenderer>().enabled = true;
            }
            if (transform.parent != null && rb.isKinematic == false)
            {
                rb.isKinematic = true;
                if(transform.parent.name == "BlasterGameObj")
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
                //make invisible
            }
            if (transform.parent != null)
            {
                if (transform.parent.name == "BlasterGameObj")
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
                //make invisible
            }
            if (unlimited_ammo == true)
            {
                ammoCount = 30;
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "MagSnapPoint")
            {
                Debug.Log("first check");
                for (int i = 0; hand.AttachedObjects.Count > i; i++)
                {
                    Debug.Log("second check");
                    print(hand.AttachedObjects[i].attachedObject.name);
                    print("(" + hand.AttachedObjects[i].attachedObject.tag + ")");
                    if (FindObjectOfType<Gun>().currentMag == null)
                    {
                        Debug.Log("1");
                        x = hand.AttachedObjects[i].attachedObject.gameObject;
                        Debug.Log("2");
                        hand.DetachObject(hand.AttachedObjects[i].attachedObject, false);
                        Debug.Log("3");
                        x.transform.parent = blaster.gameObject.transform;
                        Debug.Log("4");
                        FindObjectOfType<Gun>().currentMag = x;
                        print(transform.position);
                        Debug.Log("third check");
                        isLoaded = true;
                        Debug.Log(isLoaded);

                    }
                }
            }
        }
    }
}
