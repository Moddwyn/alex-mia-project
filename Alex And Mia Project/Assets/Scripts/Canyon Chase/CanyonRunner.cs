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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!started)
                started = true;
            left = !left;
        } 
        runner.position = Vector3.Lerp(runner.position, new Vector3(started?(left?-5:5):0,runner.position.y, runner.position.z), Time.deltaTime*moveSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Obstacle")
        {
            OnLoose?.Invoke();
        }
    }
}
