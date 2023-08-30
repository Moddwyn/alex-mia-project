using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class ContractChaseManager : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnCaught;
    [ReadOnly] public bool gameStarted;
    [ReadOnly] public bool gameEnded;

    [Space(20)]
    public TMP_Text timeText;
    public TMP_Text timeCaughtText;
    public TMP_Text timeHighText;
    public TMP_Text timeTextFinal;
    public float timeCaught;
    [ReadOnly] public float time;
    [ReadOnly] public float timeHigh;

    [HorizontalLine]
    [ReadOnly][SerializeField] float timeSinceLastSpacePress = 0f;
    [ReadOnly][SerializeField] float currTimeScale;
    [ReadOnly][SerializeField] float timeBeforeCaught;

    GameInfoHolder gameInfoHolder;

    void Start() 
    {
        gameInfoHolder = GameInfoHolder.Instance;
        timeHigh = PlayerPrefs.GetFloat(gameInfoHolder.exerciseInfo.scoreSaveKey);
        OnCaught?.AddListener(SaveGame);
    }

    public void StartGame()
    {
        timeBeforeCaught = timeCaught;

        gameStarted = true;
        OnGameStart?.Invoke();
    }

    void Update()
    {
        if (Input.anyKeyDown && !gameStarted) StartGame();

        UpdateTimeControls();
        UpdateTexts();
    }

    void UpdateTimeControls()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameStarted && !gameEnded)
        {
            timeSinceLastSpacePress = 0;
            timeBeforeCaught = timeCaught;
        }
        timeSinceLastSpacePress += Time.deltaTime;

        Time.timeScale = timeSinceLastSpacePress >= 2? 0.05f : 1;

        time += gameStarted && !gameEnded && Time.timeScale == 1?
                Time.deltaTime : 0;

        timeBeforeCaught -= gameStarted && !gameEnded && Time.timeScale == 0.05f?
                Time.unscaledDeltaTime : 0;
        timeBeforeCaught = Mathf.Clamp(timeBeforeCaught, 0, Mathf.Infinity);
        

        currTimeScale = Time.timeScale;

        if(gameStarted && !gameEnded && timeBeforeCaught <= 0)
        {
            gameEnded = true;
            OnCaught?.Invoke();
        }
    }

    void UpdateTexts()
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        System.TimeSpan tH = System.TimeSpan.FromSeconds(timeHigh);
        if (t.Seconds < 10)
        {
            timeText.text = "Time: " + t.Minutes + ":0" + t.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        else
        {
            timeText.text = "Time: " + t.Minutes + ":" + t.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }

        if (tH.Seconds < 10)
        {
            timeHighText.text = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":0" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        else
        {
            timeHighText.text = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }

        timeCaughtText.text = Time.timeScale == 0.05f?
                            "Time before caught: " + timeBeforeCaught.ToString("F2") : "";
    }

    public void SaveGame()
    {
        gameEnded = true;
        GameSaver.SaveHighScoreFloat(gameInfoHolder.exerciseInfo.scoreSaveKey, time);

        timeHigh = PlayerPrefs.GetFloat(gameInfoHolder.exerciseInfo.scoreSaveKey);

        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        System.TimeSpan tH = System.TimeSpan.FromSeconds(timeHigh);
        string finalTime;
        if (t.Seconds < 10)
        {
            finalTime = t.Minutes + ":0" + t.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        else
        {
            finalTime = t.Minutes + ":" + t.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }

        string finalTimeHigh;
        if (tH.Seconds < 10)
        {
            finalTimeHigh = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":0" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        else
        {
            finalTimeHigh = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        timeTextFinal.text = "Final Time: " + finalTime + "\n" + finalTimeHigh;
    }
    
    void OnApplicationQuit()
    {
        SaveGame();
    }
}
