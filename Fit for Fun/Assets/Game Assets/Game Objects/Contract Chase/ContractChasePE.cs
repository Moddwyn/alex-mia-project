using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractChasePE : MonoBehaviour
{
    Transform rAnkle;
    Transform lAnkle;
    bool switchPose;
    bool run;

    ContractChaseManager manager;

    void Start() {
        manager = ContractChaseManager.Instance;
    }

    void Update() {
        PostEstimateUpdate();
    }

    void PostEstimateUpdate()
    {
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

        if (lAnkle != null && rAnkle != null && PoseEstimator.Instance.ready)
        {
            if (lAnkle.GetComponent<MeshRenderer>().enabled && lAnkle.GetComponent<MeshRenderer>().enabled)
            {
                if ((rAnkle.position.y >= lAnkle.position.y) && !switchPose)
                {
                    run = true;
                    manager.PerformRunAction();
                    switchPose = true;
                }
                if ((lAnkle.position.y >= rAnkle.position.y) && switchPose)
                {
                    switchPose = false;
                }
            }
        }
    }
}
