using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemeMachine
{
    public class ItemReseter : MonoBehaviour
    {
        public Quaternion startingRotation;
        public Vector3 startingPosition;
        bool isMag = false;

        void Start()
        {
            startingPosition = transform.position;
            startingRotation = transform.rotation;
            if (gameObject.CompareTag("Mag"))
            {
                isMag = true;
            }
        }
            
        public void ResetItems()
        {
            if (isMag)
            {
                try
                {
                    transform.parent = null;
                    GetComponent<MagazineScript>().ammoCount = MagazineScript.STARTINGAMMOCOUNT;
                    GetComponent<MagazineScript>().gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    FindObjectOfType<Gun>().currentMag = null;
                    MagazineScript.isLoaded = false;
                    FindObjectOfType<Gun>().UpdateAmmoText();
                    GetComponent<MeshRenderer>().enabled = true;
                }
                catch (Exception e)
                {

                }

            }
            transform.position = startingPosition;
            transform.rotation = startingRotation;
        }
    }
}
