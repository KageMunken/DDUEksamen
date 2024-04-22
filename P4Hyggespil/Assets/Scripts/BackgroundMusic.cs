using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<AudioSource>().clip = audioClip;
            collision.gameObject.GetComponent<AudioSource>().Play();
        } 
    }
}
