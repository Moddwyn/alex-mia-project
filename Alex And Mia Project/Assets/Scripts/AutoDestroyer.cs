using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float time = 5;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", time);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
