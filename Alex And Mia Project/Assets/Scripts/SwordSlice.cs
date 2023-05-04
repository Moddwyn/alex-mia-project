using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlice : MonoBehaviour
{
    public enum Hand { Left, Right };
    public Hand hand;
    public float distanceHit;
    public ParticleSystem rockExplodeParticle;

    RaycastHit hit;
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
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, distanceHit))
        {
            if(hit.transform.GetComponent<SphereCollider>() != null)
            {
                Instantiate(rockExplodeParticle, hit.transform.position, Quaternion.identity);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
