using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemeMachine
{
    public class Bullet : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(0, 0, 50 * Time.deltaTime);           
        }
        public void DestroyBullet(float time)
        {
            Destroy(gameObject, time);
        }

        private void OnCollisionEnter(Collision collision)
        {
            print("Hit Something");
            switch (collision.gameObject.tag)
            {
                case "Enviroment":
                    Destroy(gameObject);
                    print("hit Enviroment");
                    break;
                case "Enemy":
                    Destroy(gameObject);
                    print("hit enemy");
                    collision.gameObject.GetComponent<EnemyScript>().DamageEnemy(1);
                    break;
            }
        }

    }
   
}
