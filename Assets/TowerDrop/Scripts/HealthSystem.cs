﻿using System.Collections.Generic;
using UnityEngine;

namespace TowerDrop
{
    public class HealthSystem : Singleton< MonoBehaviour>
    {
        public  List<AI_pathing> AIObjects=  new List<AI_pathing>();

        public GameObject Player_hitbox;


        private void UpdatePlayerHealth()
        {
            if (Player_hitbox == null)
                return;

            if (Player_hitbox.GetComponent<PlayerHealth>().hit == true)
                Player_hitbox.GetComponent<PlayerHealth>().playerHP--;

            Player_hitbox.GetComponent<PlayerHealth>().hit = false;

        }

        private void UpdateAIHealth()
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

        void Update()
        {

            UpdatePlayerHealth();
            UpdateAIHealth();
            
        }

    }
}