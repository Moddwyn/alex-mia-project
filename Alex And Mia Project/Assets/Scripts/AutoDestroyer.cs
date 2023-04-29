using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float timeTillDestroy = 30;

    void Start()
    {
        Invoke("DestroyObject", timeTillDestroy);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}   
