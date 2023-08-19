using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public static void SaveHighScoreInt(string key, int currentScore)
    {
        if(currentScore > PlayerPrefs.GetInt(key, 0))
            PlayerPrefs.SetInt(key, currentScore);
    }

    public static void SaveHighScoreFloat(string key, float currentScore)
    {
        if(currentScore > PlayerPrefs.GetFloat(key, 0))
            PlayerPrefs.SetFloat(key, currentScore);
    }

    public static void SaveLowScoreInt(string key, int currentScore)
    {
        if(currentScore < PlayerPrefs.GetInt(key, int.MaxValue))
            PlayerPrefs.SetInt(key, currentScore);
    }

    public static void SaveLowScoreFloat(string key, float currentScore)
    {
        if(currentScore < PlayerPrefs.GetFloat(key, float.MaxValue))
            PlayerPrefs.SetFloat(key, currentScore);
    }

    public void DeleteAllSaves() => PlayerPrefs.DeleteAll();
}
