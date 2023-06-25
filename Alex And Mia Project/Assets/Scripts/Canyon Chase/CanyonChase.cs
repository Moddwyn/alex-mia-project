using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonChase : MonoBehaviour
{
    public GameObject chunk;

    void Start()
    {
        for(int x = 0; x < 100; x++)
        {
            Instantiate(chunk, new Vector3(0,0,x*80), Quaternion.identity);
        }
        
    }

    void Update()
    {

    }
}
