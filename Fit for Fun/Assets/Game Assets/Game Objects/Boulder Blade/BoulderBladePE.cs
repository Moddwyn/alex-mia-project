using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBladePE : MonoBehaviour
{
    Transform rWrist;
    Transform lWrist;
    Transform rLeg;
    Transform lLeg;
    Transform nose;

    bool swingL;
    bool swingR;

    BoulderBladePlayer player;

    void Start() {
        player = BoulderBladePlayer.Instance;
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
                if ((lWrist.position.y > nose.position.y) && !swingL)
                {
                    player.SliceAction(1);
                    swingL = true;
                }
                if ((rWrist.position.y > nose.position.y) && !swingR)
                {
                    player.SliceAction(2);
                    swingR = true;
                }


                if ((lWrist.position.y < nose.position.y) && swingL)
                {
                    swingL = false;
                }
                if ((rWrist.position.y < nose.position.y) && swingL)
                {
                    swingR = false;
                }
            }
        }
    }
}
