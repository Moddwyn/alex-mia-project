using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeBusterPE : MonoBehaviour
{
    Transform rHip;
    Transform lHip;
    Transform rKnee;
    Transform lKnee;

    bool switchPose;

    BlazeBusterManager manager;

    void Start()
    {
        manager = BlazeBusterManager.Instance;
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
            if (rKnee == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("rightKnee"))
                    rKnee = GameObject.Find("rightKnee").transform;
            }
            if (lKnee == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("leftKnee"))
                    lKnee = GameObject.Find("leftKnee").transform;
            }

            if (lHip != null && rHip != null && lKnee != null && rKnee != null && PoseEstimator.Instance.ready)
            {
                if ((lKnee.position.y < lHip.position.y) && (rKnee.position.y < rHip.position.y) && !switchPose)
                {
                    manager.PerformWaterAction();
                    switchPose = true;
                }
                if ((lKnee.position.y > lHip.position.y) && (rKnee.position.y > rHip.position.y) && switchPose)
                {
                    switchPose = false;
                }
            }
        }
    }
}
