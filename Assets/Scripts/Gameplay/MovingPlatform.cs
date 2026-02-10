using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }

    [Header("Settings")]
    public MoveDirection direction = MoveDirection.Horizontal;
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the offset based on time, speed, and distance
        float offset = Mathf.Sin(Time.time * speed) * distance;

        // Apply movement based on selected direction
        if (direction == MoveDirection.Horizontal)
        {
            transform.position = startPos + Vector3.right * offset;
        }
        else // Vertical
        {
            transform.position = startPos + Vector3.up * offset;
        }
    }

    // Optional: Draw the path in the editor to visualize movement range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 endPos = (direction == MoveDirection.Horizontal) 
            ? transform.position + Vector3.right * distance 
            : transform.position + Vector3.up * distance;

        Gizmos.DrawLine(transform.position - (endPos - transform.position), endPos);
    }
}