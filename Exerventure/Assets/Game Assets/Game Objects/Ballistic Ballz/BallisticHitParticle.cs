using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BallisticHitParticle : MonoBehaviour
{
    ParticleSystem particle;
    BallisticBallzManager manager;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        manager = BallisticBallzManager.Instance;
    }

    void Update()
    {
        if(!manager.groundHitPool.pool.Contains(gameObject) && !particle.isPlaying)
        {
            manager.groundHitPool.InsertToPool(gameObject);
        }
    }
}
