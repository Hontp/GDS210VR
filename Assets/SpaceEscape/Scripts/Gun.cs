using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool shoot;
    public GameObject bullet;
    public void Shoot()
    {
        Vector3 angleInfo = transform.rotation.eulerAngles + new Vector3(-90, 0, 0);
        GameObject shot = Instantiate(bullet,transform.position + transform.forward*0.55f, Quaternion.Euler(angleInfo.x, angleInfo.y, angleInfo.z));
        shot.GetComponent<Rigidbody>().velocity = transform.forward * 100f;
        shot.GetComponent<Bullet>().DestroyBullet(3f);
    }


    private void Update()
    {
        if (shoot)
        {
            shoot = false;
            Shoot();
        }
    }




}
