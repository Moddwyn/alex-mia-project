using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public int moveSpeed;

    CanyonChase canyonChase;

    void Start()
    {
        canyonChase = CanyonChase.Instance;
    }

    void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;

        if(transform.position.z <= -80)
        {
            canyonChase.chunks.Add(canyonChase.SpawnChunk(new Vector3(0,0,canyonChase.chunks[^1].transform.position.z + 80)));
            Destroy(gameObject);
        }
    }

}
