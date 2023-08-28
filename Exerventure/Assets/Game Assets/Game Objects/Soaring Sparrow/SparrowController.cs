using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class SparrowController : MonoBehaviour
{
    Rigidbody bird;
    public UnityEvent OnDeath;
    public float jumpForce;

    [HorizontalLine]
    public AudioSource sfxSource;
    public AudioClip flapSound;
    public AudioClip hitSound;
    public AudioClip deadSound;

    [HorizontalLine]
    public Transform cam;
    public float transitionTime;
    public bool allowCamFollow;

    SoaringSparrowManager manager;

    void Start()
    {
        bird = GetComponent<Rigidbody>();
        bird.isKinematic = true;

        manager = SoaringSparrowManager.Instance;
        manager.OnGameStart?.AddListener(GameStartInit);

        OnDeath?.AddListener(Die);
    }

    void GameStartInit()
    {
        allowCamFollow = true;
    }

    void Update()
    {
        if (manager.gameStarted)
        {
            BirdMovement();
            CameraMovement();
        }
    }

    void CameraMovement()
    {
        if (allowCamFollow)
            cam.position = Vector3.MoveTowards(cam.position,
            new Vector3(cam.position.x, bird.position.y, bird.position.z),
            Time.deltaTime * transitionTime);
    }

    void BirdMovement()
    {
        bird.isKinematic = !manager.gameStarted;

        if (Input.GetKeyDown(KeyCode.Space) && !manager.gameEnded && manager.gameStarted)
        {
            Flap();
        }
    }

    public void Flap()
    {
        bird.velocity = Vector3.up * jumpForce;
        sfxSource.PlayOneShot(flapSound);
    }

    void Die()
    {
        allowCamFollow = false;
        
        sfxSource.PlayOneShot(hitSound);
        sfxSource.PlayOneShot(deadSound);
        bird.GetComponent<Collider>().enabled = false;
        bird.GetComponent<Animator>().SetBool("Death", true);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Pipe"))
        {
            OnDeath?.Invoke();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe Point"))
        {
            manager.score++;
        }
    }

}
