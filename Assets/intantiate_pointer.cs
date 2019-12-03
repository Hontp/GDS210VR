using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiCutter;
using Valve.VR.InteractionSystem;

public class intantiate_pointer : MonoBehaviour
{
    public GameObject pointer;
    public GameManager gm;
    GameObject refrence_pointer;
    bool spaned_ponter;
    public Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.dead == true)
        {
            if (spaned_ponter == false)
            {
                refrence_pointer=Instantiate(pointer);
                spaned_ponter = true;
                refrence_pointer.transform.position = hand.gameObject.transform.position;
                refrence_pointer.transform.rotation = hand.gameObject.transform.rotation;
                hand.AttachObject(refrence_pointer, hand.GetBestGrabbingType(), Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOffGravity | Hand.AttachmentFlags.DetachOthers);
            }

        }
        else
        {
            if (spaned_ponter == true)
            {
                hand.DetachObject(refrence_pointer);
                Destroy(refrence_pointer);
            }
        }
    }
}
