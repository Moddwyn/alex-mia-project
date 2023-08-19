using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CanyonChaseManager : Singleton<CanyonChaseManager>
{
    public CanyonPlayer player;
    [ReadOnly] public string scoreSaveKey = "CanyonHigh";
    
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

    void Start()
    {
        InitChunks();

        timeHigh = PlayerPrefs.GetFloat(scoreSaveKey);

        player.OnDeath?.AddListener(SaveGame);
    }

    void StartGame()
    {
        gameStarted = true;
        OnGameStart?.Invoke();
    }

    void Update()
    {
        UpdateTimeText();

        if (Input.anyKeyDown && !gameStarted) StartGame();
    }

    void UpdateTimeText()
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        System.TimeSpan tH = System.TimeSpan.FromSeconds(timeHigh);
        if (t.Seconds < 10)
        {
            timeText.text = "Time: " + t.Minutes + ":0" + t.Seconds;
        }
        else
        {
            timeText.text = "Time: " + t.Minutes + ":" + t.Seconds;
        }

        if (tH.Seconds < 10)
        {
            timeHighText.text = "Highest Time: " + tH.Minutes + ":0" + tH.Seconds;
        }
        else
        {
            timeHighText.text = "Highest Time: " + tH.Minutes + ":" + tH.Seconds;
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
        GameSaver.SaveHighScoreFloat(scoreSaveKey, time);

        timeHigh = PlayerPrefs.GetFloat(scoreSaveKey);

        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        System.TimeSpan tH = System.TimeSpan.FromSeconds(timeHigh);
        string finalTime = "";
        string finalTimeHigh = "";
        if (t.Seconds < 10)
        {
            finalTime = t.Minutes + ":0" + t.Seconds;
        }
        else
        {
            finalTime = t.Minutes + ":" + t.Seconds;
        }
        if (tH.Seconds < 10)
        {
            finalTimeHigh = tH.Minutes + ":0" + tH.Seconds;
        }
        else
        {
            finalTimeHigh = tH.Minutes + ":" + tH.Seconds;
        }
        timeTextFinal.text = "Final Time: " + finalTime + "\nHighest Time: " + finalTimeHigh;
    }
    
    void OnApplicationQuit()
    {
        SaveGame();
    }
    
}
