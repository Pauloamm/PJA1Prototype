using System;
using UnityEngine;

[Serializable]
public class MovementInfo 
{
    // Current position
    public Vector3 position;

    // Current orientation
    public float orientation;
    public Vector2 orientationV2;

    // Movement direction and speed
    public Vector3 velocity;

    // Rotation direction and speed
    public float rotation;
    public Vector2 rotationV2;


}
