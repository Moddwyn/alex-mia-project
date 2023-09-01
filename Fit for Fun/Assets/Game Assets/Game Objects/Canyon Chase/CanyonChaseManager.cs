using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CanyonChaseManager : Singleton<CanyonChaseManager>
{
    public CanyonPlayer player;
    
    [HorizontalLine]
    public UnityEvent OnGameStart;
    [ReadOnly] public bool gameStarted;
    [ReadOnly] public bool gameEnded;

    [HorizontalLine]
    public TMP_Text timeText;
    public TMP_Text timeHighText;
    public TMP_Text timeTextFinal;
    [ReadOnly] public float time;
    [ReadOnly] public float timeHigh;

    [HorizontalLine]
    public GameObject chunk;
    public List<GameObject> obstacles;
    public int startChunkCount = 10;
    public int spacing = 80;
    public float forwardSpeed;
    public List<Transform> spawnedChunks;

    [HorizontalLine]
    public AudioSource sfxSource;
    public AudioClip playerMoveClip;

    GameInfoHolder gameInfoHolder;

    void Start() 
    {
        gameInfoHolder = GameInfoHolder.Instance;
        InitChunks();

        timeHigh = PlayerPrefs.GetFloat(gameInfoHolder.exerciseInfo.scoreSaveKey);

        player.OnDeath?.AddListener(SaveGame);
    }

    public void StartGame()
    {
        gameStarted = true;
        OnGameStart?.Invoke();
    }

    void Update()
    {
        UpdateTimeText();
    }

    void UpdateTimeText()
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

        if (gameStarted && !gameEnded)
            time += Time.deltaTime;
    }

    void InitChunks()
    {
        for (int i = 0; i < startChunkCount; i++)
        {
            SpawnChunk();
        }
    }

    public void SpawnChunk()
    {
        if (spawnedChunks.Count == 0) return;

        Vector3 newPos = new(0, 0, spawnedChunks[^1].position.z + spacing);
        Transform newChunk = Instantiate(chunk, newPos, Quaternion.identity).transform;
        spawnedChunks.Add(newChunk);

        for (int i = -4; i <= 4; i++)
        {
            int randLeft = Random.Range(0, 2);
            int randObst = Random.Range(0, obstacles.Count);
            Transform newObstacle = Instantiate(obstacles[randObst],
            Vector3.zero, obstacles[randObst].transform.rotation).transform;
            newObstacle.SetParent(newChunk.transform);
            newObstacle.transform.localPosition = new Vector3(randLeft == 0 ? -0.625f : 0.625f, newObstacle.transform.localPosition.y, i);
        }
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
