using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonChasePE : MonoBehaviour
{
    Transform rHip;
    Transform lHip;
    Transform rAnkle;
    Transform lAnkle;

    bool switchPoseL;
    bool switchPoseR;

    CanyonPlayer manager;

    void Start()
    {
        manager = CanyonPlayer.Instance;
    }

    void Update()
    {
        PostEstimateUpdate();
    }

    void PostEstimateUpdate()
    {
        if (PoseEstimator.Instance != null)
        {
            if (lHip == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("leftHip"))
                    lHip = GameObject.Find("leftHip").transform;
            }
            if (rHip == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("rightHip"))
                    rHip = GameObject.Find("rightHip").transform;
            }
            if (rAnkle == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("rightAnkle"))
                    rAnkle = GameObject.Find("rightAnkle").transform;
            }
            if (lAnkle == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("leftAnkle"))
                    lAnkle = GameObject.Find("leftAnkle").transform;
            }

            if (lHip != null && rHip != null && lAnkle != null && rAnkle != null && PoseEstimator.Instance.ready)
            {
                if ((lAnkle.position.y < lHip.position.y) && (rAnkle.position.y < rHip.position.y) && !switchPoseL)
                {
                    manager.PerformSideAction(1);
                    switchPoseL = true;
                    switchPoseR = false;
                }
                if ((lAnkle.position.y > lHip.position.y) && (rAnkle.position.y > rHip.position.y) && !switchPoseR)
                {
                    manager.PerformSideAction(2);
                    switchPoseR = true;
                    switchPoseL = false;
                }
            }
        }
    }
}
