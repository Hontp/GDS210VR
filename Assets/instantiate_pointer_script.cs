using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace TowerDrop
{
    public class instantiate_pointer_script : MonoBehaviour
    {
        public GameObject pointer;
        public Hand hand;
        public game_maneger gm;
        // Start is called before the first frame update
        void Start()
        {
            hand = GetComponent<Hand>();
            Instantiate(pointer);
        }

        // Update is called once per frame
        void Update()
        {
            if (gm.game_phase != 0 || gm.game_phase != 3)
            {
                pointer.transform.position = hand.transform.position;
                pointer.transform.rotation = hand.transform.rotation;
                hand.AttachObject(pointer, hand.GetBestGrabbingType(), Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOffGravity | Hand.AttachmentFlags.DetachOthers);
            }
            else
            {
                hand.DetachObject(pointer);
            }
        }
    }
}
