using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using MemeMachine;


public class rightHand : MonoBehaviour
{
    public GameObject actualBlaster;
    public GameObject dummyBlaster;
    public Hand hand;
    public GameObject x;
    public GameObject laser;
    public GameObject slimHand;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        //hand.renderModelPrefab = laser;
        //hand.SetRenderModel(laser);
        canvas = GameObject.Find("Canvas");

    }

    // Update is called once per frame
    void Update()
    {
       
        //game start change from laser to slimhand
       if (/*hand.renderModelPrefab == laser && */canvas.GetComponent<MenuSystem>().gamePlaying)
       {
            laser.SetActive(false);
            //hand.DetachObject(laser);
           // Destroy(GameObject.Find("LaserPointer(Clone)"));
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
            if (!Gun.gunBackGripGrabbed && hand.AttachedObjects[i].attachedObject.name == "Dummy Blaster")
            {
                Destroy(hand.AttachedObjects[i].attachedObject.gameObject);

                actualBlaster.SetActive(true);
                Gun.gunBackGripGrabbed = true;
                actualBlaster.GetComponent<Interactable>().highlightOnHover = false;
                /*
                x = hand.AttachedObjects[i].attachedObject.gameObject;

                hand.DetachObject(hand.AttachedObjects[i].attachedObject,false);
               
                Destroy(x.GetComponent<Throwable>());
                Destroy(x.GetComponent<Rigidbody>());

                x.GetComponent<Interactable>().highlightOnHover = false;
                //x.gameObject.transform.position = Vector3.zero;
                x.transform.parent = hand.gameObject.transform;
               // x.gameObject.transform.rotation = hand.transform.rotation;
                
                Debug.Log("grabbed");
                Gun.gunBackGripGrabbed = true;
               */
            }
        }        
    }
    public void MenuActive()
    {
        actualBlaster.SetActive(false);
        Gun.gunBackGripGrabbed = false;
        laser.SetActive(true);
        GameObject dummy = Instantiate<GameObject>(dummyBlaster, new Vector3(-0.612f, 1.228f, 35.4f), Quaternion.Euler(-30, 180,0));

    }
}
