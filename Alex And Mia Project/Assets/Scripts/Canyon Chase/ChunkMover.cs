using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public int moveSpeed;

    bool touched;

    void Update()
    {
        if(touched == false)
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        } else
        {
            transform.position = Vector3.Lerp(transform.position, GameObject.Find("Black Hole").transform.position, Time.deltaTime);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pipe Chunk Border")
        {
            touched = true;
        }
    }
}
