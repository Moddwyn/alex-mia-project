using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonChase : MonoBehaviour
{
    public GameObject chunk;
    public List<GameObject> chunks;
    public List<GameObject> obstacles;

    public static CanyonChase Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for(int x = 0; x < 10; x++)
        {
            chunks.Add(SpawnChunk(new Vector3(0,0,x*80)));
        }
        
    }

    void Update()
    {
        chunks.RemoveAll(item => item == null);
    }

    public GameObject SpawnChunk(Vector3 pos)
    {
        GameObject newChunk = Instantiate(chunk, pos, Quaternion.identity);

        for (int i = -4; i <= 4; i++)
        {
            int randLeft = Random.Range(0,2);
            int randObst = Random.Range(0, obstacles.Count);
            Transform newObstacle = Instantiate(obstacles[randObst],
            Vector3.zero, obstacles[randObst].transform.rotation).transform;
            newObstacle.SetParent(newChunk.transform);
            newObstacle.transform.localPosition = new Vector3(randLeft ==0?-0.625f:0.625f, newObstacle.transform.localPosition.y, i);
        }
        return newChunk;
    }
}
