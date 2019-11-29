using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace MemeMachine
{
    public class MagazineScript : MonoBehaviour
    {
        public Hand hand;
        public GameObject x;
        public GameObject blaster;
        public static bool isLoaded = false;

        public Rigidbody rb;
        bool attached;

        public int ammoCount = 30;
        public bool hasAmmo = true;



        // Start is called before the first frame update
        void Start()
        {
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
            }
            if (transform.parent != null && rb.isKinematic == false)
            {
                rb.isKinematic = true;
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
                    if (hand.AttachedObjects[i].attachedObject.CompareTag("Mag"))
                    {
                        Debug.Log("1");
                        x = hand.AttachedObjects[i].attachedObject.gameObject;
                        Debug.Log("2");
                        hand.DetachObject(hand.AttachedObjects[i].attachedObject, false);
                        Debug.Log("3");
                        x.transform.parent = blaster.gameObject.transform;
                        Debug.Log("4");
                        FindObjectOfType<Gun>().currentMag = x;
                        Debug.Log("third check");
                        isLoaded = true;
                        Debug.Log(isLoaded);

                    }
                }
            }
        }
    }
}
