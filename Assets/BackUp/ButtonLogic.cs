using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public int sceneToLoad;
    Vector3 startingPos;
    float x, z;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        x = startingPos.x;
        z = startingPos.z;



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HeightLimiter();
        ButtonMove();
    }

    void HeightLimiter()
    {
        if (transform.position.y > 1.4f)
        {
            transform.position = new Vector3(x, 1.4f, z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (transform.position.y < 1.22f)
        {
            transform.position = new Vector3(x, 1.226f, z);
            Invoke("ChangeScene", 0.1f);
        }
    }
    void ButtonMove()
    {
        if(transform.position.y < 1.4f)
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
