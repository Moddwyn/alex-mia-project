using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sprinting : MonoBehaviour
{
    public float distance;
    public float time;
    public bool gameStart;

    [Space(20)]
    public TMP_Text timeText;
    public TMP_Text distanceText;

    float slowTime = 2;
    float timeTillSlow;

    Transform rHip;
    Transform lHip;
    Transform rKnee;
    Transform lKnee;
    bool switchPose;
    bool run;

    public void StartGame()
    {
        gameStart = true;
        timeTillSlow = slowTime;
    }

    void Update()
    {
        timeText.text = "Time Left: " + time.ToString("F2");
        distanceText.text = "Distance: " + distance.ToString("F2");

        if(gameStart)
        {
            Movements();
            time -= Time.unscaledDeltaTime;
            timeTillSlow -= Time.unscaledDeltaTime;
        }
    }

    void Movements()
    {
        if(rHip == null && PoseEstimator.Instance.ready)
        {
            if(GameObject.Find("rightHip"))
                rHip = GameObject.Find("rightHip").transform;
        }
        if(lHip == null && PoseEstimator.Instance.ready)
        {
            if(GameObject.Find("leftHip"))
                lHip = GameObject.Find("leftHip").transform;
        }
        if(rKnee == null && PoseEstimator.Instance.ready)
        {
            if(GameObject.Find("rightKnee"))
                rKnee = GameObject.Find("rightKnee").transform;
        }
        if(lKnee == null && PoseEstimator.Instance.ready)
        {
            if(GameObject.Find("leftKnee"))
                lKnee = GameObject.Find("leftKnee").transform;
        }

        if(rHip != null && lHip != null && rKnee != null && lKnee != null && PoseEstimator.Instance.ready)
        {
            if((rKnee.position.y >= rHip.position.y) && !switchPose)
            {
                run = true;
                timeTillSlow = slowTime;
                switchPose = true;
            }
            if((lKnee.position.y <= lHip.position.y) && switchPose)
            {
                switchPose = false;
            }
        }

        if (run && time > 0)
        {
            Time.timeScale = 1;
            distance += Time.deltaTime;
        }

        if(timeTillSlow <= 0)
        {
            run = false;
            Time.timeScale = 0.05f;
        }
    }
}
