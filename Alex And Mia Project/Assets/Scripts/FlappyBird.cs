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

    void Start()
    {
        SpawnPipes();
    }

    void Update()
    {
        cam.position = Vector3.MoveTowards(cam.position, 
        new Vector3(cam.position.x, bird.position.y, bird.position.z), 
        Time.deltaTime * transitionTime);

        BirdMovement();
    }

    void BirdMovement()
    {
        bird.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            bird.velocity = Vector3.up * jumpForce;
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
}
