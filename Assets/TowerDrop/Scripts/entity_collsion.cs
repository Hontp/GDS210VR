using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDrop
{
    public class entity_collsion : MonoBehaviour
    {
        public int entity_index;
        public AI_pathing AI_P;
        bool hit = false;
        bool inlist;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "wepon" && hit == false)
            {
                hit = true;
                AI_P.RemoveFromEntityList(entity_index);
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Destroy(gameObject, 5);
            }
        }
    }
}
