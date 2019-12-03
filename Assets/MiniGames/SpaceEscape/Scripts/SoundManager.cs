using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    public AudioSource audio;

    // Use this for initialization
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }


}