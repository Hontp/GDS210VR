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
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "hand")
            {
                col.GetComponent<search_rubble>().search = true;
                

            }
        }
        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == "hand")
            {
                col.gameObject.GetComponent<search_rubble>().search = false;
               

            }
        }

    }
}
