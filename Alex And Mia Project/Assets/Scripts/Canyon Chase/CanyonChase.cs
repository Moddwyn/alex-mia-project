using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonChase : MonoBehaviour
{
    public GameObject chunk;
    public List<GameObject> chunks;

    public static CanyonChase Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for(int x = 0; x < 10; x++)
        {
            chunks.Add(Instantiate(chunk, new Vector3(0,0,x*80), Quaternion.identity));
        }
        
    }

    void Update()
    {
        chunks.RemoveAll(item => item == null);
    }
}
