using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public List<TrackFollow> spawnedBouldersRight = new List<TrackFollow>();
    public List<TrackFollow> spawnedBouldersLeft = new List<TrackFollow>();
    public Transform[] boulders;
    public Vector2 spawnDelay;
    public float speed = 20;
    public CinemachineSmoothPath leftTrack;
    public CinemachineSmoothPath rightTrack;

    public static BoulderSpawner Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnBoulder(leftTrack));
        StartCoroutine(SpawnBoulder(rightTrack));
    }

    IEnumerator SpawnBoulder(CinemachineSmoothPath path)
    {
        Vector3 pos = path.EvaluatePositionAtUnit(0, CinemachinePathBase.PositionUnits.Distance);
        Transform spawned = Instantiate(boulders[Random.Range(0, boulders.Length)], pos, Quaternion.identity);
        TrackFollow follow = spawned.AddComponent<TrackFollow>();
        follow.m_Path = path;
        follow.m_Speed = speed;
        follow.m_Position = 0;
        follow.independentRot = true;

        if(path == leftTrack) spawnedBouldersLeft.Add(follow);
        else spawnedBouldersRight.Add(follow);

        yield return new WaitForSeconds(Random.Range(spawnDelay.x, spawnDelay.y));
        StartCoroutine(SpawnBoulder(path));
    }

    void Update()
    {
        RemoveMissingOrEmptyItems();
    }

    private void RemoveMissingOrEmptyItems()
    {
        for (int i = spawnedBouldersRight.Count - 1; i >= 0; i--)
        {
            if (spawnedBouldersRight[i] == null || spawnedBouldersRight[i].gameObject == null)
            {
                spawnedBouldersRight.RemoveAt(i);
            }
        }
        for (int i = spawnedBouldersLeft.Count - 1; i >= 0; i--)
        {
            if (spawnedBouldersLeft[i] == null || spawnedBouldersLeft[i].gameObject == null)
            {
                spawnedBouldersLeft.RemoveAt(i);
            }
        }
    }
}
