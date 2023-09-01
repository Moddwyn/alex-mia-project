using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class PreviewHandler : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text exerciseText;
    public TMP_Text summaryText;
    public TMP_Text recordScoreText;
    public VideoPlayer animationPlayer;
    public VideoPlayer gamePreviewPlayer;


    GameInfoHolder gameInfoHolder;
    void Start() {
        gameInfoHolder = GameInfoHolder.Instance;
    }

    public void SetPreview()
    {
        ExerciseInfo currentInfo;
        if (gameInfoHolder != null && gameInfoHolder.exerciseInfo != null)
        {
            currentInfo = gameInfoHolder.exerciseInfo;
            titleText.text = currentInfo.title;
            exerciseText.text = currentInfo.exercise;
            summaryText.text = currentInfo.gameSummary;
            
            animationPlayer.clip = currentInfo.animationVideo;
            StartCoroutine(animationPlayer.GetComponent<VideoGifPlayer>().PlayVideo());
            gamePreviewPlayer.clip = currentInfo.gamePreviewVideo;

            recordScoreText.text = currentInfo.recordScoreHeader + ": " + gameInfoHolder.GetSavedScore() + " " + currentInfo.recordScoreUnits;
        }
    }
}
