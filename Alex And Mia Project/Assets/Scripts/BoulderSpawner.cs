using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public Transform[] boulders;
    public Vector2 sizeRange;
    public Collider spawnArea;
    public float spawnDelay;

    int spawnAmt;

    void Start()
    {
        spawnAmt = 3;
        StartCoroutine(SpawnBoulder());
    }

    IEnumerator SpawnBoulder()
    {
        for (int i = 0; i < spawnAmt; i++)
        {
            Transform spawned = Instantiate(boulders[Random.Range(0, boulders.Length)], GetRandomSpawnPosition(), Quaternion.identity);
            spawned.localScale = Vector3.one * Random.Range(sizeRange.x, sizeRange.y);
        }
        yield return new WaitForSeconds(spawnDelay);
        spawnAmt++;
        StartCoroutine(SpawnBoulder());
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.size;
        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float y = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);
        return new Vector3(x, y, z);
    }
}
