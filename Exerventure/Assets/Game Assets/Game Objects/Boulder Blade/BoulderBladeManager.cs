using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BoulderBladeManager : Singleton<BoulderBladeManager>
{
    public BoulderBladePlayer player;
    public ObjectPooler boulderPool;
    [ReadOnly] public bool canSpawn;

    [HorizontalLine]
    public CinemachineSmoothPath pathLeft;
    public CinemachineSmoothPath pathRight;

    [HorizontalLine]
    public int score;
    [ReadOnly] public int highScore;
    [ReadOnly][SerializeField] string scoreSaveKey = "BoulderHigh";
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text scoreTextFinal;

    [HorizontalLine]
    public UnityEvent OnGameStart;
    [ReadOnly] public bool gameStarted;

    [HorizontalLine]
    public Vector2 spawnIntervals;
    [ReadOnly] public int currentSpeedLevel;
    public float speedChangeIntervals;
    public List<Vector2> speedRange;

    void Start()
    {
        highScore = PlayerPrefs.GetInt(scoreSaveKey);
        player.OnDeath?.AddListener(SaveGame);
    }

    void StartGame()
    {
        gameStarted = true;
        canSpawn = true;
        player.canSlice = true;
        OnGameStart?.Invoke();

        StartCoroutine(StartSpawning());
        StartCoroutine(SpeedChange());
    }

    void Update()
    {
        if (Input.anyKeyDown && !gameStarted) StartGame();

        scoreText.text = "Score: "  + score;
        highScoreText.text = "High Score: " + highScore;
    }

    IEnumerator StartSpawning()
    {
        if (canSpawn)
        {
            if(Random.Range(0 ,2) == 0) SpawnBoulder(pathLeft);
            if(Random.Range(0 ,2) == 0) SpawnBoulder(pathRight);
        }
        yield return new WaitForSeconds(Random.Range(spawnIntervals.x, spawnIntervals.y));
        StartCoroutine(StartSpawning());
    }

    void SpawnBoulder(CinemachineSmoothPath track)
    {
        Vector3 pos = track.EvaluatePositionAtUnit(0, CinemachinePathBase.PositionUnits.Distance);
        TrackFollow newBall = boulderPool.GrabFromPool(pos, Quaternion.identity).GetComponent<TrackFollow>();
        newBall.m_Position = 0;
        newBall.m_Path = track;

        Vector2 currentSpeed = speedRange[currentSpeedLevel];
        newBall.m_Speed = Random.Range(currentSpeed.x, currentSpeed.y);
    }

    IEnumerator SpeedChange()
    {
        yield return new WaitForSeconds(speedChangeIntervals);
        if (currentSpeedLevel < speedRange.Count - 1)
            currentSpeedLevel++;

        if(currentSpeedLevel >= speedRange.Count - 1) currentSpeedLevel = speedRange.Count - 1;

        StartCoroutine(SpeedChange());
    }

    void SaveGame()
    {
        canSpawn = false;
        GameSaver.SaveHighScoreInt(scoreSaveKey, score);

        highScore = PlayerPrefs.GetInt(scoreSaveKey);
        scoreTextFinal.text = "Final Score: "  + score + "\nHigh Score: " + highScore;
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }
}
