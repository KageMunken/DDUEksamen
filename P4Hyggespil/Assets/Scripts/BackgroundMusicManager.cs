using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] backgroundMusic;

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Cave")
        {
            musicSource.clip = backgroundMusic[0];
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Outside")
        {
            musicSource.clip = backgroundMusic[1];
            musicSource.Play();
        }
    }
}
