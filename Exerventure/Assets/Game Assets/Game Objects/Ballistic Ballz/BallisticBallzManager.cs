using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BallisticBallzManager : Singleton<BallisticBallzManager>
{
    public BallisticBallzPlayer player;
    public List<CinemachineSmoothPath> paths;
    public ObjectPooler ballPool;
    [ReadOnly] public bool canSpawn;

    [HorizontalLine]
    public int score;
    [ReadOnly] public int highScore;
    [ReadOnly][SerializeField] string scoreSaveKey = "BallisticHigh";
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text scoreTextFinal;

    [HorizontalLine]
    public UnityEvent OnGameStart;
    [ReadOnly] public bool gameStarted;

    [HorizontalLine]
    public Vector2 spawnIntervals;
    public Vector2 spawnAmtRange;
    [ReadOnly] public int currentSpeedLevel;
    public float speedChangeIntervals;
    public List<Vector2> speedRange;

    [HorizontalLine]
    public ObjectPooler groundHitPool;
    public AudioSource sfxSource;
    public AudioClip shootClip;
    public AudioClip hitSound;

    void Start()
    {
        highScore = PlayerPrefs.GetInt(scoreSaveKey);
        player.OnDeath?.AddListener(SaveGame);
    }

    void StartGame()
    {
        gameStarted = true;
        canSpawn = true;
        player.canMove = true;
        OnGameStart?.Invoke();

        if (paths.Count > 0)
        {
            StartCoroutine(StartSpawning());
            StartCoroutine(SpeedChange());
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && !gameStarted) StartGame();

        scoreText.text = "Score: "  + score;
        highScoreText.text = "High Score: " + highScore;
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(Random.Range(spawnIntervals.x, spawnIntervals.y));
        
        if (canSpawn)
        {
            for (int i = 0; i < Random.Range(spawnAmtRange.x, spawnAmtRange.y); i++)
            {
                CinemachineDollyCart newBall = ballPool.GrabFromPool(Vector3.zero, Quaternion.identity).GetComponent<CinemachineDollyCart>();
                newBall.m_Position = 0;
                newBall.m_Path = paths[Random.Range(0, paths.Count)];

                Vector2 currentSpeed = speedRange[currentSpeedLevel];
                newBall.m_Speed = Random.Range(currentSpeed.x, currentSpeed.y);

                sfxSource.PlayOneShot(shootClip);
            }
        }

        StartCoroutine(StartSpawning());
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
