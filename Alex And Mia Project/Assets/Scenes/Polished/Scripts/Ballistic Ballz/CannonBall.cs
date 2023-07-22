using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    
    void Update()
    {
        if(GetComponent<CinemachineDollyCart>() != null)
        {
            if(GetComponent<CinemachineDollyCart>().m_Position == 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
