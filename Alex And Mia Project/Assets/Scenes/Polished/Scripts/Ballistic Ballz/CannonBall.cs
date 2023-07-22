using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip shootSound;
    public AudioClip fallingSound;
    public AudioClip hitSound;

    void Start()
    {
        sound.PlayOneShot(shootSound);
        sound.PlayOneShot(fallingSound);
    }
    void Update()
    {
        if(GetComponent<CinemachineDollyCart>() != null)
        {
            if(GetComponent<CinemachineDollyCart>().m_Position == 1)
            {
                
                BallisticBallz.Instance.audioSource.pitch = Random.Range(0.3f, 1.6f);
                BallisticBallz.Instance.audioSource.PlayOneShot(hitSound);
                Destroy(gameObject);
            }
        }
    }
}
