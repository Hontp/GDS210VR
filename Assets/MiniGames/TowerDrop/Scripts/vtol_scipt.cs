using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vtol_scipt : MonoBehaviour
{
    public Animator anim;
    public bool grabed;
    public bool hit;
    public bool open_close;
   
    public GameObject player;
    public float distance_to_shoot;

    public bool shootimg;
    float shoot_tick;
    public float fire_rate;

    public GameObject projectile;

    public Transform[] vtol_arial_points;
    int arialpoint;
    public float jet_speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        List<Transform> arial_points_list = new List<Transform>();
        for (int i = 0; GameObject.FindGameObjectsWithTag("arial point").Length > i; i++)
        {
            arial_points_list.Add(GameObject.FindGameObjectsWithTag("arial point")[i].transform);
        }
        vtol_arial_points = arial_points_list.ToArray();
    }

    // Update is called once per frame
    void Update()
    {



        anim.SetBool("grabed", grabed);
        anim.SetBool("open_Close", open_close);

        if (grabed == false && hit == false)
        {

            if (open_close == false)
            {
                Vector3 direction = -(transform.position - player.transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, player.transform.up), 0.2f);
                transform.position = Vector3.Lerp(transform.position, vtol_arial_points[arialpoint].position, 0.02f);
                shootimg = true;

            }
            else
            {
                transform.position += transform.forward * Time.deltaTime * jet_speed;
                Vector3 direction = -(transform.position - player.transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, player.transform.up), 0.2f);

            }
            if (Vector3.Distance(transform.position, vtol_arial_points[arialpoint].position) < 5)
            {
                arialpoint = Random.Range(0, vtol_arial_points.Length);
            }

            if (Vector3.Distance(transform.position, player.transform.position) < distance_to_shoot)
            {
                shootimg = true;
                open_close = false;
            }
            if (transform.parent != null && transform.parent.tag == "hand")
            {
                grabed = true;
                gameObject.tag = "wepon";
            }
            if (shootimg == true)
            {

                shoot_tick += Time.deltaTime;
                if (shoot_tick >= fire_rate)
                {
                    GameObject x;
                    shoot_tick = 0;
                    x = Instantiate(projectile, transform.forward * 3 + transform.position, transform.rotation);
                    x.transform.parent = null;
                    Destroy(x, 10);
                }
            }
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "wepon")
        {
            hit = true;
        }
    }
}
