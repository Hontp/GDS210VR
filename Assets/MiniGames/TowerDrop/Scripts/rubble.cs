using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class rubble : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "hand")
            {
                GameObject hand = col.gameObject;
                hand.GetComponent<search_rubble>().search = true;

            }
        }

    }
}
