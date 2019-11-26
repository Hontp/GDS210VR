using UnityEngine;

namespace TowerDrop
{
    public class Health : MonoBehaviour
    {
        public int entity_index;
        public AI_pathing AI_P;
        public bool hit = false;
        bool inlist;

        [SerializeField]
        private int hp = 5;

        [SerializeField]
        private float armor = 1;

        [SerializeField]
        private float totalHP;

        private void OnEnable()
        {
            totalHP = hp + armor;
        }

        public float HP
        {
            get
            {
                return totalHP;
            }
            set
            {
                totalHP = value;
            }
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
