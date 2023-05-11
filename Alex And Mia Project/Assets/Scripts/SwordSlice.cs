using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlice : MonoBehaviour
{
    public enum Hand { Left, Right };
    public Hand hand;
    public float distanceHit = 290;
    public float distanceMax = 308;
    public ParticleSystem rockExplodeParticle;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hand == Hand.Right)
        {
            GetComponent<Animator>().SetTrigger("Slice");
            GetComponentInChildren<ParticleSystem>().Play();
        }
        if (Input.GetMouseButtonDown(1) && hand == Hand.Left)
        {
            GetComponent<Animator>().SetTrigger("Slice");
            GetComponentInChildren<ParticleSystem>().Play();

        }
    }
    public void Slice()
    {
        if(hand == Hand.Right)
        {
            if(BoulderSpawner.Instance.spawnedBouldersLeft[0].m_Position >= distanceHit &&
            BoulderSpawner.Instance.spawnedBouldersLeft[0].m_Position <= distanceMax)
            {
                Instantiate(rockExplodeParticle, BoulderSpawner.Instance.spawnedBouldersLeft[0].transform.position, Quaternion.identity);
                Destroy(BoulderSpawner.Instance.spawnedBouldersLeft[0].transform.gameObject);
            }
        }
        if(hand == Hand.Left)
        {
            if(BoulderSpawner.Instance.spawnedBouldersRight[0].m_Position >= distanceHit &&
            BoulderSpawner.Instance.spawnedBouldersRight[0].m_Position <= distanceMax)
            {
                Instantiate(rockExplodeParticle, BoulderSpawner.Instance.spawnedBouldersRight[0].transform.position, Quaternion.identity);
                Destroy(BoulderSpawner.Instance.spawnedBouldersRight[0].transform.gameObject);
            }
        }
    }
}
