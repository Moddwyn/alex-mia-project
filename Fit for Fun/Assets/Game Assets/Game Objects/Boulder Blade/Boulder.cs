using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TrackFollow))]
public class Boulder : MonoBehaviour
{
    public UnityEvent OnBreak;
    public int damage;
    public float finishDistance;
    [ReadOnly][SerializeField] bool broken;

    [HorizontalLine]
    public ParticleSystem breakParticle;

    TrackFollow trackFollow;
    BoulderBladeManager manager;

    void Start()
    {
        trackFollow = GetComponent<TrackFollow>();
        manager = BoulderBladeManager.Instance;
    }

    void Update()
    {
        if (trackFollow.m_Position >= finishDistance)
        {
            Break(true);
        }
    }

    public void Break(bool damaged)
    {
        if (!broken)
        {
            broken = true;
            OnBreak?.Invoke();

            breakParticle.Play();
            if(damaged) manager.player.DoDamage(damage);
            else manager.score++;

            trackFollow.m_Speed = 0;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            StartCoroutine(BreakDelete());
        }
    }

    IEnumerator BreakDelete()
    {
        yield return new WaitForSeconds(2);
        breakParticle.Stop();
        manager.boulderPool.InsertToPool(gameObject);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        broken = false;
    }
}
