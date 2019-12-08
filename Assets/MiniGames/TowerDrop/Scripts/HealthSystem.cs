using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDrop
{
    public class HealthSystem : Singleton< MonoBehaviour>
    {
        public RectTransform healthbar;

        public  List<AI_pathing> AIObjects=  new List<AI_pathing>();

        public GameObject Player_hitbox;

        public PlayerHealth Ph;

        void Start()
        {
             Ph = Player_hitbox.GetComponent<PlayerHealth>();
        }

        private void UpdatePlayerHealth()
        {
            if (Player_hitbox == null)
                return;

            if (Ph.hit == true)
                Ph.playerHP--;

            Ph.hit = false;
            
            

            if (Ph.playerHP <= 0 && GetComponent<game_maneger>().game_phase == 2)
            {
                GetComponent<game_maneger>().game_phase=3;
            }
            if (GetComponent<game_maneger>().game_phase == 0)
            {
                Ph.playerHP = 20;
            }
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
                        {
                            entity.GetComponent<Health>().HP--;

                            entity.GetComponent<Health>().hit = false;
                        }
                    }
                }
            }
        }

        void Update()
        {
            

            UpdatePlayerHealth();
            UpdateAIHealth();


            healthbar.localScale = new Vector3(Ph.playerHP / 100, healthbar.localScale.y, healthbar.localScale.z);

        }

    }
}