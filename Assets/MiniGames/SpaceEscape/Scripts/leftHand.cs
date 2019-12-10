using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using MemeMachine;

public class leftHand : MonoBehaviour
{
    public Hand hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; hand.AttachedObjects.Count > i; i++)
        {
            if (hand.AttachedObjects[i].attachedObject.CompareTag("Mag") && MagazineScript.isLoaded)
            {
                MagazineScript.isLoaded = false;
                FindObjectOfType<Gun>().currentMag = null;
                hand.AttachedObjects[i].attachedObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
