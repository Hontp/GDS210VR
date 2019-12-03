using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiCutter;
using Valve.VR.InteractionSystem;

public class Instantiate_Pointer : MonoBehaviour
{
    public GameObject pointer;
    public GameManager gm;
    GameObject reference_pointer;
    bool spawned_pointer;
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
            if (spawned_pointer == false)
            {
                reference_pointer=Instantiate(pointer);
                spawned_pointer = true;
                reference_pointer.transform.position = hand.gameObject.transform.position;
                reference_pointer.transform.rotation = hand.gameObject.transform.rotation;
                hand.AttachObject(reference_pointer, hand.GetBestGrabbingType(), Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOffGravity | Hand.AttachmentFlags.DetachOthers);
            }

        }
        else
        {
            if (spawned_pointer == true)
            {
                hand.DetachObject(reference_pointer);
                Destroy(reference_pointer);
            }
        }
    }
}
