using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
                break;
        }
    }

}
