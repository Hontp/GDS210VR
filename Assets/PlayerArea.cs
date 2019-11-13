using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemeMachine
{
    public class PlayerArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider objectEntering)
        {
            if (objectEntering != null && objectEntering.CompareTag("Enemy"))
            {
                objectEntering.gameObject.GetComponent<EnemyScript>().DamageEnemy(10000);
                print("An enemy has reached the player");
            }
        }
    }

}
