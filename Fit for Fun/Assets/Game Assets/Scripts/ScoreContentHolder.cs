using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreContentHolder : MonoBehaviour
{
    public ExerciseInfo exerciseInfo;

    [HorizontalLine]
    public TMP_Text titleText;
    public TMP_Text exerciseText;
    public Image iconImage;

    public TMP_Text highScoreText;
    [ReadOnly] public string currentHighScore;

    void Start()
    {
        titleText.text = exerciseInfo.title;
        exerciseText.text = exerciseInfo.exercise;
        iconImage.sprite = exerciseInfo.icon;

        
    }

    void Update()
    {   if(gameObject.activeInHierarchy)
            UpdateCurrentScore();
    }

    void UpdateCurrentScore()
    {
        currentHighScore = GetSavedScore();
        highScoreText.text = exerciseInfo.recordScoreHeader + ": " + currentHighScore + " " + exerciseInfo.recordScoreUnits;
    }

    public string GetSavedScore()
    {
        if(exerciseInfo != null)
        {
            if(exerciseInfo.scoreType == ExerciseInfo.ScoreType.Integer)
            {
                return PlayerPrefs.GetInt(exerciseInfo.scoreSaveKey).ToString();
            }
            if(exerciseInfo.scoreType == ExerciseInfo.ScoreType.Float)
            {
                return PlayerPrefs.GetFloat(exerciseInfo.scoreSaveKey).ToString("F2");
            }
        }

        return "";
    }
}
