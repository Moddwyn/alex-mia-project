using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sprinting : MonoBehaviour
{
    public float distance;
    public float points;
    public float time;

    [Header("Debug")]
    public bool gameStart;
    public bool bonesActive;

    [Space(20)]
    public TMP_Text timeText;
    public TMP_Text distanceText;
    public TMP_Text pointsText;

    float slowTime = 2;
    float timeTillSlow;
    int pointsAdd;

    Transform rAnkle;
    Transform lAnkle;
    bool switchPose;
    bool run;

    public void StartGame()
    {
        gameStart = true;
        timeTillSlow = slowTime;
        points = 0;
    }

    void Update()
    {
        timeText.text = "Time Left: " + time.ToString("F2");
        distanceText.text = "Distance: " + distance.ToString("F2");
        pointsText.text = "Points: " + points;

        if (gameStart)
        {
            Movements();
            time -= Time.unscaledDeltaTime;
            timeTillSlow -= Time.unscaledDeltaTime;
        }
    }

    void Movements()
    {
        if (rAnkle == null && PoseEstimator.Instance.ready)
        {
            if (GameObject.Find("rightAnkle"))
                rAnkle = GameObject.Find("rightAnkle").transform;
        }
        if (lAnkle == null && PoseEstimator.Instance.ready)
        {
            if (GameObject.Find("leftAnkle"))
                lAnkle = GameObject.Find("leftAnkle").transform;
        }

        if (lAnkle != null && rAnkle != null && PoseEstimator.Instance.ready)
        {
            if (lAnkle.GetComponent<MeshRenderer>().enabled && lAnkle.GetComponent<MeshRenderer>().enabled)
            {
                bonesActive = true;
                if ((rAnkle.position.y >= lAnkle.position.y) && !switchPose)
                {
                    run = true;
                    timeTillSlow = slowTime;
                    pointsAdd += 1;
                    switchPose = true;
                }
                if ((lAnkle.position.y >= rAnkle.position.y) && switchPose)
                {
                    switchPose = false;
                }
            } else
                bonesActive = false;
        } else bonesActive = false;

        if (run && time > 0)
        {
            Time.timeScale = 1;
            distance += Time.deltaTime;
            points += (int)(Time.deltaTime + pointsAdd);
        }

        if (timeTillSlow <= 0)
        {
            run = false;
            Time.timeScale = 0.05f;
        }
    }
}
