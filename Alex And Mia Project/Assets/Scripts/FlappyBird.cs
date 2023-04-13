using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    public Rigidbody bird;
    public Transform cam;

    [Space(20)]
    public float transitionTime;
    public float jumpForce;
    public float forwardSpeed;

    [Space(20)]
    public GameObject pipePrefab;       // Prefab of the pipe to spawn
    public Vector2 pipeYSpacingRange;
    public float pipeSideSpacing; 
    public int amount;

    [Space(20)]
    Transform rWrist;
    Transform lWrist;
    Transform rLeg;
    Transform lLeg;
    Transform nose;

    bool switchPose;
    bool jump;
    bool dead;

    void Start()
    {
        SpawnPipes();
    }

    void Update()
    {   
        if(!dead)
        cam.position = Vector3.MoveTowards(cam.position, 
        new Vector3(cam.position.x, bird.position.y, bird.position.z), 
        Time.deltaTime * transitionTime);

        BirdMovement();
    }

    void BirdMovement()
    {
        if(!dead)
            bird.velocity = new Vector3(bird.velocity.x, bird.velocity.y, (bird.transform.forward * forwardSpeed).z);

        if(lWrist == null && PoseEstimator.Instance.ready)
        {
            if(GameObject.Find("leftWrist"))
                lWrist = GameObject.Find("leftWrist").transform;
        }
        if(rWrist == null && PoseEstimator.Instance.ready)
        {
            if(GameObject.Find("rightWrist"))
                rWrist = GameObject.Find("rightWrist").transform;
        }
        if(nose == null && PoseEstimator.Instance.ready)
        {
            if(GameObject.Find("nose"))
                nose = GameObject.Find("nose").transform;
        }

        if(lWrist != null && rWrist != null && nose != null && PoseEstimator.Instance.ready)
        {
            if((lWrist.position.y > nose.position.y) && (rWrist.position.y > nose.position.y) && !switchPose)
            {
                jump = true;
                switchPose = true;
            }
            if((lWrist.position.y < nose.position.y) && (rWrist.position.y < nose.position.y) && switchPose)
            {
                switchPose = false;
            }
        }

        if (jump && !dead)
        {
            bird.velocity = Vector3.up * jumpForce;
            print("jump");
            jump = false;
        }
    }

    void SpawnPipes()
    {
        float lastZ = 0;
        for (int i = 0; i < amount; i++)
        {
            Vector3 bottomPipePosition = new Vector3(0, -12 + Random.Range(pipeYSpacingRange.x, pipeYSpacingRange.y), lastZ);
            Vector3 topPipePosition = new Vector3(0, bottomPipePosition.y + 16, lastZ);
            Instantiate(pipePrefab, topPipePosition, Quaternion.identity);
            Instantiate(pipePrefab, bottomPipePosition, Quaternion.identity);

            lastZ += pipeSideSpacing;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Pipe")
        {
            dead = true;
            bird.GetComponent<Collider>().enabled = false;
            bird.GetComponent<Animator>().SetBool("Death", true);
        }
    }

}
