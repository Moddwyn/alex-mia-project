using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    public int score;
    public int spacing = 10;
    public Vector2 yChangeRange = new Vector2(-10, 10);
    public List<Transform> spawnedChunks = new List<Transform>();
    public int startChunkCount = 10;
    [Space(20)]
    public Rigidbody bird;
    public Transform cam;
    public AudioSource sfxSource;
    public AudioClip flapSound;
    public AudioClip hitSound;
    public AudioClip deadSound;
    public TMP_Text scoreText;

    [Space(20)]
    public float transitionTime;
    public float jumpForce;
    public float forwardSpeed;

    [Space(20)]
    public GameObject pipeChunk;

    [Space(20)]
    Transform rWrist;
    Transform lWrist;
    Transform rLeg;
    Transform lLeg;
    Transform nose;

    bool switchPose;
    bool jump;
    bool dead;

    [Space(20)]
    public bool gameStart;

    public static FlappyBird Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < startChunkCount; i++)
        {
            SpawnPipes();
        }
    }

    public void StartGame()
    {
        gameStart = true;
        score = 0;
    }

    void Update()
    {
        spawnedChunks.RemoveAll(item => item == null);

        scoreText.text = "Score: " + score;
        if (!dead)
            cam.position = Vector3.MoveTowards(cam.position,
            new Vector3(cam.position.x, bird.position.y, bird.position.z),
            Time.deltaTime * transitionTime);

        if (gameStart)
        {
            BirdMovement();
            PostEstimateUpdate();
        }

        bird.isKinematic = !gameStart;
    }

    void BirdMovement()
    {
        if (Input.GetMouseButtonDown(0)) jump = true;

        if (jump && !dead)
        {
            bird.velocity = Vector3.up * jumpForce;
            sfxSource.PlayOneShot(flapSound);
            jump = false;
        }
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
                    jump = true;
                    switchPose = true;
                }
                if ((lWrist.position.y < nose.position.y) && (rWrist.position.y < nose.position.y) && switchPose)
                {
                    switchPose = false;
                }
            }
        }
    }

    public void SpawnPipes()
    {
        Vector3 newPos = new Vector3(0, 0, spawnedChunks[spawnedChunks.Count - 1].position.z + spacing);
        Transform newChunk = Instantiate(pipeChunk, newPos, Quaternion.identity).transform;
        int changeInY = Random.Range((int)yChangeRange.x, (int)yChangeRange.y + 1);
        newChunk.GetChild(2).position += Vector3.up * changeInY;
        spawnedChunks.Add(newChunk);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Pipe")
        {
            dead = true;
            sfxSource.PlayOneShot(hitSound);
            sfxSource.PlayOneShot(deadSound);
            bird.GetComponent<Collider>().enabled = false;
            bird.GetComponent<Animator>().SetBool("Death", true);
            Invoke("RestartGame", 3);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Pipe Point")
        {
            score++;
        }
    }

}
