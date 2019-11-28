using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using MemeMachine;

public class leftHand : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject x;
    Hand hand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* for (int i = 0; hand.AttachedObjects.Count > i; i++)
        {
            if (hand.AttachedObjects[i].attachedObject.name == "Mag")
            {
                x = hand.AttachedObjects[i].attachedObject.gameObject;

                hand.DetachObject(hand.AttachedObjects[i].attachedObject, false);
                x.transform.parent = hand.gameObject.transform;
                Destroy(x.GetComponent<Throwable>());
                Destroy(x.GetComponent<Rigidbody>());

                x.GetComponent<Interactable>().highlightOnHover = false;

                x.gameObject.transform.rotation = hand.transform.rotation;
                x.gameObject.transform.position = Vector3.zero;
            
            }
        }*/
    }
}
