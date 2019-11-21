using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace TowerDrop
{
    public class search_rubble : MonoBehaviour
    {
        public SteamVR_Action_Boolean search_action;

        public Hand hand;

        public GameObject[] throwable;

        public bool search;
        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

        }
        private void Update()
        {

            if(SteamVR_Actions.default_GrabPinch[hand.handType].state && search == true &&hand.AttachedObjects.Count<1)
            {
                Instantiatethrowable();
                
                search = false;
            }
        }
        void Instantiatethrowable(){
            int x = Random.Range(1, 4);
            GameObject G;

            if (x == 1)
            {
                G=Instantiate(throwable[0], hand.transform.position, Quaternion.identity);
                G.transform.parent = hand.gameObject.transform;
                hand.AttachObject(G, hand.GetBestGrabbingType(), G.GetComponent<Throwable>().attachmentFlags);
            }
            if (x == 2)
            {
                G=Instantiate(throwable[1], hand.transform.position, Quaternion.identity);
                G.transform.parent = hand.gameObject.transform;
                hand.AttachObject(G, hand.GetBestGrabbingType(), G.GetComponent<Throwable>().attachmentFlags);
            }
            if (x == 3)
            {
                G=Instantiate(throwable[2], hand.transform.position, Quaternion.identity);
                G.transform.parent = hand.gameObject.transform;
                hand.AttachObject(G, hand.GetBestGrabbingType(), G.GetComponent<Throwable>().attachmentFlags);
            }
        }
    }
}
