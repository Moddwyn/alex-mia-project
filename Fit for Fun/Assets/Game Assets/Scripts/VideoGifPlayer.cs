using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoGifPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float delay = 1;
    
    void Start() {
        StartCoroutine(PlayVideo());
    }

    public IEnumerator PlayVideo()
    {
        while (true)
        {
            if(!videoPlayer.isPlaying)
                videoPlayer.Play();

            while (videoPlayer.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
