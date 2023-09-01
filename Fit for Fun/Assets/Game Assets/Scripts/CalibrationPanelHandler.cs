using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class CalibrationPanelHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public TMP_Text instructionsText;

    GameInfoHolder gameInfoHolder;

    void Start() {
        gameInfoHolder = GameInfoHolder.Instance;
        if(gameInfoHolder != null)
        {
            videoPlayer.clip = gameInfoHolder.exerciseInfo.animationVideo;
            StartCoroutine(videoPlayer.GetComponent<VideoGifPlayer>().PlayVideo());

            instructionsText.text = gameInfoHolder.exerciseInfo.gameInstructions;
        }
    }

}
