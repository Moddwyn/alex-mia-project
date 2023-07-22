using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeChunk : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if(FlappyBird.Instance.gameStart)
            transform.Translate(Vector3.back * speed * Time.deltaTime);

        if(transform.position.z <= -20)
        {
            FlappyBird.Instance.SpawnPipes();
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.tag == "Pipe Chunk Border")
        {
            if(FlappyBird.Instance != null)
            {
                FlappyBird.Instance.SpawnPipes();
            }
            Destroy(gameObject);
        }
    }
}
