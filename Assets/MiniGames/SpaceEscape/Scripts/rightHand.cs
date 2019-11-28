using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using MemeMachine;



public class rightHand : MonoBehaviour
{
    public Hand hand;
    public GameObject x;
    public GameObject laser;
    public GameObject slimHand;

    // Start is called before the first frame update
    private void Awake()
    {
        hand.renderModelPrefab = laser;
        hand.SetRenderModel(laser);
       
    }
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        //game start change from laser to slimhand
        if (GetComponent<MenuSystem>().gamePlaying)
        {
            hand.renderModelPrefab = slimHand;
            hand.SetRenderModel(slimHand);
        }

        //when dead put laser back in hand

        /*if (!GetComponent<MenuSystem>().isDead)
         {
            hand.renderModelPrefab = laser;
            hand.SetRenderModel(laser);
         }
         */


        for (int i=0; hand.AttachedObjects.Count > i; i++)
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
                x.gameObject.transform.position = Vector3.zero;
                //hand.SetRenderModel(x);
                //Destroy(x.GetComponent<Interactable>());
                Debug.Log("grabbed");
                Gun.gunBackGripGrabbed = true;
            }
        }
        
    }
}
