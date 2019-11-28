using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MagazineScript : MonoBehaviour
{
    public Hand hand;
    public GameObject x;
    public GameObject blaster;
    public static bool isLoaded = false;

    public Rigidbody rb;
    bool attached;

    public static int ammoCount = 30;
    public bool hasAmmo = true;
    
    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("LeftHand").gameObject.GetComponent<Hand>();
        blaster = GameObject.Find("BlasterGameObj");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attached == true)
            Destroy(GetComponent<Rigidbody>());
        if (gameObject.transform.parent == null)
            attached = true;
            //when not in mag or hand
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MagSnapPoint")
        {
            Debug.Log("first check");
            for (int i = 0; hand.AttachedObjects.Count > i; i++)
            {
                if (hand.AttachedObjects[i].attachedObject.name == "Mag")
                {
                    Debug.Log("second check");
                    //GetComponent<Gun>().currentMag = x.gameObject;
                   // Gun.currentMag = x.gameObject;
                    x = hand.AttachedObjects[i].attachedObject.gameObject;

                    hand.DetachObject(hand.AttachedObjects[i].attachedObject, false);
                    x.transform.parent = blaster.gameObject.transform;
                    x.GetComponent<Throwable>().enabled = false;
                    Destroy(GetComponent<Throwable>());
                    Destroy(rb);
                    
                    isLoaded = true;
                    Debug.Log(isLoaded);
                    
                }
            }
        }
    }
}
