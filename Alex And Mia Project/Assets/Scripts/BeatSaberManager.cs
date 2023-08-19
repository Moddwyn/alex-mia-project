using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatSaberManager : MonoBehaviour
{
    public int score;
    public float timer;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public static BeatSaberManager Instance;
    

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timerText.text = "Time Left: " + timer.ToString("F2");
        scoreText.text = "Score: " + score;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
