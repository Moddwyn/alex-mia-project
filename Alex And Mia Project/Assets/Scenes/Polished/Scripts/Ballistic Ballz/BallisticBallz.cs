using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticBallz : MonoBehaviour
{
    public CinemachineSmoothPath[] tracks;
    public CinemachineDollyCart ball;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 2);
    }

    // Update is called once per frame
    void Spawn()
    {

        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            CinemachineDollyCart b = Instantiate(ball);
            b.m_Path = tracks[Random.Range(0, tracks.Length)];
        }

    }
}
