using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanyonRunner : MonoBehaviour
{
    public UnityEvent OnLoose;
    public Transform runner;
    public float moveSpeed;

    bool left;
    bool started;
    bool loose;
    float time;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !loose)
        {
            if (!started)
            {
                started = true;
                time = 0;
            }
            left = !left;
        }

        TimeSpan t = TimeSpan.FromSeconds(time);
        if(t.Seconds < 10) {
            CanyonChase.Instance.timeText.text = t.Minutes+":0"+t.Seconds;
        }
        else {
            CanyonChase.Instance.timeText.text = t.Minutes+":"+t.Seconds;
        }

        if(!loose)
            time += Time.deltaTime;
        runner.position = Vector3.Lerp(runner.position, new Vector3(started ? (left ? -5 : 5) : 0, runner.position.y, runner.position.z), Time.deltaTime * moveSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Obstacle" && !loose)
        {
            loose = true;
            OnLoose?.Invoke();
        }
    }

    public bool IsLoose() => loose;
}
