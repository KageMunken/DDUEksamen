using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    [SerializeField] VideoPlayer myVideoPlayer;


    // Start is called before the first frame update
    void Start()
    {
        myVideoPlayer.loopPointReached += DoSomethingWhenVideoFinish;
    }

    void DoSomethingWhenVideoFinish(VideoPlayer vp)
    {
        gameObject.GetComponent<SetupGame>().Setup();
        gameObject.GetComponent<SceneChange>().changeScene();
    }
}
