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
        CinemachineDollyCart b = Instantiate(ball);
        b.m_Path = tracks[Random.Range(0, tracks.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
