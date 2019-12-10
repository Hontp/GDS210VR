using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public int sceneToLoad;
    Vector3 startingPos;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        x = startingPos.x;
        z = startingPos.z;
        y = startingPos.y;



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HeightLimiter();
        ButtonMove();
    }

    void HeightLimiter()
    {
        if (transform.position.y > y)
        {
            transform.position = new Vector3(x, y, z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (transform.position.y < y - 0.2f)
        {
            transform.position = new Vector3(x, y-0.2f, z);
            Invoke("ChangeScene", 0.1f);
        }
    }
    void ButtonMove()
    {
        if(transform.position.y < y)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 1.5f);
        }
    }
    void ChangeScene()
    {
        Destroy(GameObject.Find("Player"));
        SceneManager.LoadScene(sceneToLoad);
    }



}
