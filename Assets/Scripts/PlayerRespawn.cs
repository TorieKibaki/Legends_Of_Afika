using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    public void Respawn()
    {
        transform.position = startPosition;
    }
}
