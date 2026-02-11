using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement Values")]
    public float moveSpeed = 3f;
    public float jumpForce = 7f;

    [Header("Physics Improvements")]
    [Tooltip("Gravity multiplier when falling (Higher = faster fall)")]
    public float fallMultiplier = 2f;
    
    [Tooltip("Gravity multiplier when releasing jump early (Higher = shorter hop)")]
    public float lowJumpMultiplier = 2f;

    [Header("Assists")]
    [Tooltip("Time after walking off a ledge where you can still jump")]
    public float coyoteTime = 0.2f;
    [Tooltip("Time before hitting the ground where a jump input is registered")]
    public float jumpBufferTime = 0.2f;

    [Header("Detection Settings")]
    public float groundRadius = 0.2f;
}