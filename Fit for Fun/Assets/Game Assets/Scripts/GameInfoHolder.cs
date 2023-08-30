using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoHolder : Singleton<GameInfoHolder>
{
    protected override void Awake() 
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public ExerciseInfo exerciseInfo;

    public void SetGameInfo(ExerciseInfo info) => exerciseInfo = info;
    public void PlayCurrentExercise() => SceneManager.LoadScene(exerciseInfo.sceneIndex);

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
