using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using MemeMachine;


public class handcheak : MonoBehaviour
{
    public Hand hand;
    public GameObject x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; hand.AttachedObjects.Count > i; i++)
        {
            if (hand.AttachedObjects[i].attachedObject.name == "BlasterGameObj")
            {
                x = hand.AttachedObjects[i].attachedObject.gameObject;

                hand.DetachObject(hand.AttachedObjects[i].attachedObject,false);
                x.transform.parent = hand.gameObject.transform;
                Destroy(x.GetComponent<Throwable>());
                Destroy(x.GetComponent<Rigidbody>());

                x.GetComponent<Interactable>().highlightOnHover = false;

                x.gameObject.transform.rotation = hand.transform.rotation;

                //hand.SetRenderModel(x);
                //Destroy(x.GetComponent<Interactable>());
                Debug.Log("grabbed");
                Gun.gunBackGripGrabbed = true;
            }
        }
        
    }
}
