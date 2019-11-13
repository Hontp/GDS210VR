using UnityEngine;

namespace TowerDrop
{
    public class Singleton<T> : MonoBehaviour where T : Object
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                return instance;
            }
        }


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this as T;
        }
    }
}

