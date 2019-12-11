using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{

    public class entity_maneger : MonoBehaviour
    {
        // Start is called before the first frame update
        public int maxSpidertank=3;

        public AI_pathing[] spidertank_pathing;

        public bool SpawnSpiderTank;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (spidertank_pathing[0].Single_entity != null && spidertank_pathing[1].Single_entity != null && spidertank_pathing[2].Single_entity != null)
            {
                SpawnSpiderTank = false;
            }
            else
            {
                SpawnSpiderTank = true;
            }
        }
    }
}