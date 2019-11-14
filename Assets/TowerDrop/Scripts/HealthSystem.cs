using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class HealthSystem : Singleton< MonoBehaviour>
    {
        public  List<AI_pathing> AIObjects=  new List<AI_pathing>();

        void Update()
        {
            if (AIObjects == null)
                return;
            for (int i = 0; AIObjects.Count > i; i++)
            {

                for (int x = 0; x < AIObjects[i].entitys.Count; x++)
                {
                    
                     GameObject entity = AIObjects[i].entitys[x].AI_gameObject;

                    if (entity != null)
                    {
                        if (entity.GetComponent<Health>().hit)
                            entity.GetComponent<Health>().HP--;

                        entity.GetComponent<Health>().hit = false;
                    }
                }
            }
            
        }
    }
}