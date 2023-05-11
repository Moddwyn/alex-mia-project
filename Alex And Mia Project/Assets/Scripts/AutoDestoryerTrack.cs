using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestoryerTrack : MonoBehaviour
{
    TrackFollow follow;
    void Update()
    {
        if(GetComponent<TrackFollow>() != null)
            follow = GetComponent<TrackFollow>();

        if(follow != null && follow.m_Position >= follow.m_Path.PathLength - 1)
        {
            Destroy(gameObject);
        }
    }
}
