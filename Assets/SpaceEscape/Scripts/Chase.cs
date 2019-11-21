using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform player;
    static Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < 30)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("Idel", false);
            if (direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("Run", true);
                anim.SetBool("Attack", false);
            }
            else
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Run", false);
            }
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
        }
        
    }
}
