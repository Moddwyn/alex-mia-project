using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonRunner : MonoBehaviour
{
    public Transform runner;
    public float moveSpeed;

    bool left;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) left = !left;
        runner.position = Vector3.Lerp(runner.position, new Vector3(left?-5:5,runner.position.y, runner.position.z), Time.deltaTime*moveSpeed);
    }
}
