using UnityEngine;

namespace TowerDrop
{
    public class Health : MonoBehaviour
    {
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
    }
}