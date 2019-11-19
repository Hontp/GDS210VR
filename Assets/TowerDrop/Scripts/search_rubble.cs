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

            if(SteamVR_Actions.default_GrabPinch[hand.handType].state && search == true)
            {
                instantiatethrowable();
                search = false;
            }
        }
        void instantiatethrowable(){
            int x = Random.Range(1, 4);

            if (x == 1)
            {
                Instantiate(throwable[0], hand.transform.position, Quaternion.identity);
            }
            if (x == 2)
            {
                Instantiate(throwable[1], hand.transform.position, Quaternion.identity);
            }
            if (x == 3)
            {
                Instantiate(throwable[2], hand.transform.position, Quaternion.identity);
            }
        }
    }
}
