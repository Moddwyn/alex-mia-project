using UnityEngine;

public class PipeChunk : MonoBehaviour
{
    bool allowMovement;

    SoaringSparrowManager manager;

    void Start()
    {
        manager = SoaringSparrowManager.Instance;
    }

    void Update()
    {
        allowMovement = manager.gameStarted && !manager.gameEnded;
        if(allowMovement)
            transform.Translate(Vector3.back * manager.forwardSpeed * Time.deltaTime);

        if(transform.position.z <= -20)
        {
            manager.SpawnPipes();
            Destroy(gameObject);
        }
    }
}
