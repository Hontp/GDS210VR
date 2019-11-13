using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class HealthSystem : Singleton< MonoBehaviour>
    {
        public AI_pathing AIObjects;

        void Update()
        {
            if (AIObjects == null)
                return;

            for ( int i =0;  i <  AIObjects.entitys.Count; i++)
            {
                GameObject entity = AIObjects.entitys[i].AI_gameObject;

                if (entity != null)
                {
                    if (entity.GetComponent<entity_collsion>().hit)
                        entity.GetComponent<Health>().HP--;

                    entity.GetComponent<entity_collsion>().hit = false;
                }               
            }
            
        }
    }
}