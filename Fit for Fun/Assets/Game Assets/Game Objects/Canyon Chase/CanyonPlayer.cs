using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class CanyonPlayer : Singleton<CanyonPlayer>
{
    public UnityEvent OnDeath;
    [HorizontalLine]
    public float sideSpeed;

    CanyonChaseManager manager;

    bool left;

    void Start()
    {
        manager = CanyonChaseManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            left = !left;
            manager.sfxSource.PlayOneShot(manager.playerMoveClip);
        }

        transform.position = Vector3.Lerp(transform.position
        , new Vector3(manager.gameStarted ? (left ? -5 : 5) : 0
        , transform.position.y, transform.position.z)
        , Time.deltaTime * sideSpeed);
    }

    public void PerformSideAction(int side)
    {
        if (manager.gameStarted && !manager.gameEnded)
        {
            left = side == 1;
            manager.sfxSource.PlayOneShot(manager.playerMoveClip);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Obstacle" && !manager.gameEnded)
        {
            OnDeath?.Invoke();
        }
    }
}
