using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int damage;
    BallisticBallzManager manager;

    void Start()
    {
        manager = BallisticBallzManager.Instance;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Island"))
        {
            manager.score++;
            manager.sfxSource.PlayOneShot(manager.hitSound);

            Vector3 collisionPoint = other.ClosestPointOnBounds(transform.position);
            ParticleSystem hitParticle = manager.groundHitPool.GrabFromPool(collisionPoint, Quaternion.identity)
                                        .GetComponent<ParticleSystem>();
            hitParticle.Play();

            manager.ballPool.InsertToPool(gameObject);
        }

        if(other.CompareTag("Player"))
        {
            manager.player.DoDamage(damage);
        }
    }
}
