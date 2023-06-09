using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideoScript : MonoBehaviour
{
    public GameObject introVideoCanvas;
    public GameObject introVideoClip;
    public VideoPlayer introVideo;

    // Start is called before the first frame update
    void Start()
    {
        introVideo.loopPointReached += VideoFinished;
    }

    void VideoFinished(VideoPlayer vp)
    {
        introVideoCanvas.SetActive(false);
        introVideoClip.SetActive(false);
    }
}
