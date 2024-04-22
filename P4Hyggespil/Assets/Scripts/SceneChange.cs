using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        changeScene();
    }

    public void changeScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}