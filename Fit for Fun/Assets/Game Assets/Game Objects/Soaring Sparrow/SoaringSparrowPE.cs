using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoaringSparrowPE : MonoBehaviour
{
    Transform rWrist;
    Transform lWrist;
    Transform rLeg;
    Transform lLeg;
    Transform nose;

    bool switchPose;

    SparrowController sparrow;

    void Start() {
        sparrow = SparrowController.Instance;
    }

    void Update() {
        PostEstimateUpdate();
    }

    void PostEstimateUpdate()
    {
        if (PoseEstimator.Instance != null)
        {
            if (lWrist == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("leftWrist"))
                    lWrist = GameObject.Find("leftWrist").transform;
            }
            if (rWrist == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("rightWrist"))
                    rWrist = GameObject.Find("rightWrist").transform;
            }
            if (nose == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("nose"))
                    nose = GameObject.Find("nose").transform;
            }

            if (lWrist != null && rWrist != null && nose != null && PoseEstimator.Instance.ready)
            {
                if ((lWrist.position.y > nose.position.y) && (rWrist.position.y > nose.position.y) && !switchPose)
                {
                    sparrow.Flap();
                    switchPose = true;
                }
                if ((lWrist.position.y < nose.position.y) && (rWrist.position.y < nose.position.y) && switchPose)
                {
                    switchPose = false;
                }
            }
        }
    }
}
