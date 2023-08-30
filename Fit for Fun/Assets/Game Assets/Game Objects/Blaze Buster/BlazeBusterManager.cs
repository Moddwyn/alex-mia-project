using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlazeBusterManager : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnFireOut;
    [ReadOnly] public bool gameStarted;
    [ReadOnly] public bool gameEnded;

    [HorizontalLine]
    public int fireLeft;
    public Image fireLeftFill;
    int maxFireLeft;

    [HorizontalLine]
    public TMP_Text timeText;
    public TMP_Text timeLowText;
    public TMP_Text timeTextFinal;
    [ReadOnly] public float time;
    [ReadOnly] public float timeLow;
    

    [HorizontalLine]
    public List<ParticleSystem> explosionParticles;
    public List<ParticleSystem> fireParticles;
    public ParticleSystem waterSprayParticle;

    [HorizontalLine]
    public AudioSource sfxSource;
    public AudioSource fireBurningSource;
    public AudioClip explodeClip;
    public AudioClip waterSpray;

    GameInfoHolder gameInfoHolder;

    void Start()
    {
        gameInfoHolder = GameInfoHolder.Instance;

        maxFireLeft = fireLeft;
        
        timeLow = PlayerPrefs.GetFloat(gameInfoHolder.exerciseInfo.scoreSaveKey);
        OnFireOut?.AddListener(SaveGame);
    }

    void StartGame()
    {
        gameStarted = true;
        OnGameStart?.Invoke();

        StartCoroutine(StartFire());
    }

    void Update()
    {
        UpdateTimeText();

        if (Input.anyKeyDown && !gameStarted) StartGame();
        if (Input.GetKeyDown(KeyCode.Space) && gameStarted && !gameEnded)
        {
            waterSprayParticle.Stop();
            waterSprayParticle.Play();
            sfxSource.PlayOneShot(waterSpray);

            RemoveFire(1);
        }

        fireLeftFill.fillAmount = Mathf.MoveTowards(fireLeftFill.fillAmount, Mathf.InverseLerp(0, maxFireLeft, fireLeft), Time.deltaTime * 2);
    }

    void RemoveFire(int amount)
    {
        fireLeft -= amount;

        if(fireLeft <= 0)
        {
            fireLeft = 0;
            OnFireOut?.Invoke();
        }

    }

    void UpdateTimeText()
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        System.TimeSpan tH = System.TimeSpan.FromSeconds(timeLow);
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
            timeLowText.text = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":0" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        else
        {
            timeLowText.text = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }



        if (gameStarted && !gameEnded)
            time += Time.deltaTime;
    }

    IEnumerator StartFire()
    {
        explosionParticles.ForEach(p=>p.Play());
        sfxSource.PlayOneShot(explodeClip);
        yield return new WaitForSeconds(1);
        fireParticles.ForEach(p=>p.Play());
        fireBurningSource.Play();
    }

    public void SaveGame()
    {
        gameEnded = true;
        GameSaver.SaveLowScoreFloat(gameInfoHolder.exerciseInfo.scoreSaveKey, time);

        timeLow = PlayerPrefs.GetFloat(gameInfoHolder.exerciseInfo.scoreSaveKey);

        System.TimeSpan t = System.TimeSpan.FromSeconds(time);
        System.TimeSpan tH = System.TimeSpan.FromSeconds(timeLow);
        string finalTime = "";
        string finalTimeLow = "";
        if (t.Seconds < 10)
        {
            finalTime = t.Minutes + ":0" + t.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        else
        {
            finalTime = t.Minutes + ":" + t.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }

        if (tH.Seconds < 10)
        {
            finalTimeLow = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":0" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        else
        {
            finalTimeLow = gameInfoHolder.exerciseInfo.recordScoreHeader + ": " + tH.Minutes + ":" + tH.Seconds + " " + gameInfoHolder.exerciseInfo.recordScoreUnits;
        }
        timeTextFinal.text = "Final Time: " + finalTime + "\n" + finalTimeLow;
    }
}
