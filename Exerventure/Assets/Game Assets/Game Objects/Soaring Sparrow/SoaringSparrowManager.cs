using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SoaringSparrowManager : Singleton<SoaringSparrowManager>
{
    public SparrowController sparrow;
    [HorizontalLine]
    public UnityEvent OnGameStart;
    [ReadOnly] public bool gameStarted;
    [ReadOnly] public bool gameEnded;
    [HorizontalLine]
    public int score;
    [ReadOnly] public int highScore;
    [ReadOnly][SerializeField] string scoreSaveKey = "SparrowHigh";
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text scoreTextFinal;

    [HorizontalLine]
    public GameObject pipeChunk;
    public int startChunkCount = 10;
    public int spacing = 10;
    public float forwardSpeed;
    public Vector2 yChangeRange = new(-10, 10);
    public List<Transform> spawnedChunks;

    void Start()
    {
        highScore = PlayerPrefs.GetInt(scoreSaveKey);

        sparrow.OnDeath?.AddListener(SaveGame);
        InitPipes();
    }

    void Update()
    {
        spawnedChunks.RemoveAll(item => item == null);
        scoreText.text = "Score: "  + score;
        highScoreText.text = "High Score: " + highScore;

        if(Input.GetKeyDown(KeyCode.Space) && !gameStarted) StartGame();
    }

    void StartGame()
    {
        gameStarted = true;
        OnGameStart?.Invoke();
    }

    void InitPipes()
    {
        for (int i = 0; i < startChunkCount; i++)
        {
            SpawnPipes();
        }
    }

    public void SpawnPipes()
    {
        if(spawnedChunks.Count == 0) return;

        Vector3 newPos = new(0, 0, spawnedChunks[^1].position.z + spacing);
        Transform newChunk = Instantiate(pipeChunk, newPos, Quaternion.identity).transform;

        int changeInY = Random.Range((int)yChangeRange.x, (int)yChangeRange.y + 1);
        newChunk.GetChild(2).position += Vector3.up * changeInY;
        spawnedChunks.Add(newChunk);
    }

    void SaveGame()
    {
        gameEnded = true;
        GameSaver.SaveHighScoreInt(scoreSaveKey, score);

        highScore = PlayerPrefs.GetInt(scoreSaveKey);
        scoreTextFinal.text = "Final Score: "  + score + "\nHigh Score: " + highScore;
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }
}
