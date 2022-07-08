using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip endLevel;

    public AudioSource _audio;

    void Start()
    {
        Destroy(gameObject, 1);
    }

    void Play(string clip)
    {
        switch (clip)
        {
            case "jump":
                _audio.PlayOneShot(jump);
                break;
            case "endLevel":
                _audio.PlayOneShot(endLevel);
                break;            
        }
    }
}
