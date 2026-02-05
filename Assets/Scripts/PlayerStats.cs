using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement Values")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Detection Settings")]
    public float groundRadius = 0.2f;
}