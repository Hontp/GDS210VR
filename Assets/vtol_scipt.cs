using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vtol_scipt : MonoBehaviour
{
    public Animator anim;
    public bool grabed;
    public bool open_close;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,player.transform.position)<0)
        {

        }
        if (transform.parent.tag == "hand")
        {
            grabed = true;
        }
    }
}
