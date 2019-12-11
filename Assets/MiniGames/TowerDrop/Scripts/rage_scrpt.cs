using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class rage_scrpt : MonoBehaviour
{
    public int rage;

    public RectTransform ragebar;

    public Hand left;
    public Hand right;
    public GameObject player;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ragebar.localScale = new Vector3((float)rage / 100, ragebar.localScale.y, ragebar.localScale.z);

        rage = Mathf.Clamp(rage, 0, 100);

        if(rage==100 && SteamVR_Actions.default_Teleport[left.handType].state && SteamVR_Actions.default_Teleport[right.handType].state)
        {
            rage = 0;
            StartCoroutine(rage_attack());
        }
    }
    IEnumerator rage_attack()
    {
        Time.timeScale = 0.2f;
        Instantiate(explosion, player.transform);
        yield return new WaitForSecondsRealtime(10);
        rage = 0;
        Time.timeScale = 1;
        
    }
}
