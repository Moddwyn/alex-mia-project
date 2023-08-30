using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonChunk : MonoBehaviour
{
    bool allowMovement;

    CanyonChaseManager manager;

    void Start()
    {
        manager = CanyonChaseManager.Instance;
    }

    void Update()
    {
        allowMovement = manager.gameStarted && !manager.gameEnded;
        if(allowMovement)
            transform.Translate(Vector3.back * manager.forwardSpeed * Time.deltaTime);

        if(transform.position.z <= -80)
        {
            manager.SpawnChunk();
            Destroy(gameObject);
        }
    }
}
