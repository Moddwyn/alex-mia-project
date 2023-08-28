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

    public string GetSavedScore<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            if (typeof(T) == typeof(int))
            {
                return PlayerPrefs.GetInt(key).ToString();
            }
            else if (typeof(T) == typeof(float))
            {
                return PlayerPrefs.GetFloat(key).ToString();
            }
            else if (typeof(T) == typeof(string))
            {
                return PlayerPrefs.GetString(key);
            }
            else
            {
                Debug.LogWarning("Unsupported data type for PlayerPrefs key: " + typeof(T));
                return null;
            }
        }
        else
        {
            Debug.LogWarning("PlayerPrefs key not found: " + key);
            return null;
        }
    }
}
