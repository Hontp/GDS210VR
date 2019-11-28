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
        public GameObject x;
        // Start is called before the first frame update
        void Start()
        {
            hand = GetComponent<Hand>();
            x=Instantiate(pointer);
            gm = GameObject.Find("GameScene").GetComponent<game_maneger>();
        }

        // Update is called once per frame
        void Update()
        {
            if (gm.game_phase == 0 || gm.game_phase == 3)
            {
              //  x.transform.parent = hand.gameObject.transform;
                x.transform.position = hand.gameObject.transform.position;
                x.transform.rotation = hand.gameObject.transform.rotation;
                hand.AttachObject(x, hand.GetBestGrabbingType(), Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOffGravity | Hand.AttachmentFlags.DetachOthers);
            }
            else
            {
                x.transform.parent = null;
                hand.DetachObject(x);
            }
        }
    }
}
