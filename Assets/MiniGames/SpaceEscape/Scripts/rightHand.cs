﻿using System.Collections;
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
        canvas = GameObject.Find("Canvas");

    }

    // Update is called once per frame
    void Update()
    {
         


        for (int i=0; hand.AttachedObjects.Count > i; i++)
        {
            if (!Gun.gunBackGripGrabbed && hand.AttachedObjects[i].attachedObject.name == "Dummy Blaster")
            {
                if (FindObjectOfType<MenuSystem>().gamePlaying)
                {
                    Destroy(hand.AttachedObjects[i].attachedObject.gameObject);

                    actualBlaster.SetActive(true);
                    Gun.gunBackGripGrabbed = true;
                    actualBlaster.GetComponent<Interactable>().highlightOnHover = false;
                }
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
    public void GameStarted()
    {
        if (canvas.GetComponent<MenuSystem>().gamePlaying)
        {
            laser.SetActive(false);
            Debug.Log("from laser to slim");
        }
    }
}
