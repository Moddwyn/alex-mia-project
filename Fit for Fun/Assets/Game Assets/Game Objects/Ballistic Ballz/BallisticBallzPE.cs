using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticBallzPE : MonoBehaviour
{
    Transform rWrist;
    Transform lWrist;
    Transform rKnee;
    Transform lKnee;

    BallisticBallzPlayer player;

    void Start()
    {
        player = BallisticBallzPlayer.Instance;
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
            if (lKnee == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("leftKnee"))
                    lKnee = GameObject.Find("leftKnee").transform;
            }
            if (rKnee == null && PoseEstimator.Instance.ready)
            {
                if (GameObject.Find("rightKnee"))
                    rKnee = GameObject.Find("rightKnee").transform;
            }


            if (lWrist != null && rWrist != null && lKnee != null && rKnee != null && PoseEstimator.Instance.ready)
            {
                if (((lWrist.position.y >= lKnee.position.y) && (rWrist.position.y >= rKnee.position.y)) || 
                    (lWrist.position.y <= lKnee.position.y) && (rWrist.position.y <= rKnee.position.y))
                {
                    player.horizontalInput = 0;
                } else if ((lWrist.position.y < lKnee.position.y) && (rWrist.position.y >= rKnee.position.y))
                {
                    player.horizontalInput = -1;
                } else if ((lWrist.position.y >= lKnee.position.y) && (rWrist.position.y < rKnee.position.y))
                {
                    player.horizontalInput = 1;
                }
                
            }
        }
    }
}
