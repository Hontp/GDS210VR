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

        public int SpiderTankCount;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            SpiderTankCount = spidertank_pathing[0].entitys.Count + spidertank_pathing[1].entitys.Count + spidertank_pathing[2].entitys.Count;

        }
    }
}