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
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //game start change from laser to slimhand
       if (hand.renderModelPrefab == laser && canvas.GetComponent<MenuSystem>().gamePlaying)
        {
            
            Destroy(GameObject.Find("LaserPointer(Clone)"));
            hand.renderModelPrefab = slimHand;
            hand.SetRenderModel(slimHand);
         
            
            Debug.Log("from laser to slim");
        }


        //when dead put laser back in hand

       /* if (!GetComponent<EnemySpawner>().gameUnderway)
         {
            Debug.Log("from gun to slim");
            hand.renderModelPrefab = laser;
            hand.SetRenderModel(laser);
         }*/
         


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

                //hand.SetRenderModel(x);
                //Destroy(x.GetComponent<Interactable>());
                Debug.Log("grabbed");
                Gun.gunBackGripGrabbed = true;
            }
        }
        
    }
    
}
