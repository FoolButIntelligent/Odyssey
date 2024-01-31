using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats<PlayerStats>
{
    [Header("General Stats")] 
    public float rotationSpeed = 970f;
    [Header("Motion Stats")] 
    public float brakeThreshold = -0.8f;
    public float turningDrag = 30f;
    public float acceleraiton = 16f;
    public float topSpeed = 7.5f;
    public float airAcceleration = 32f;
    public float deceleration = 28f;

    [Header("Running Stats")] 
    public float runningAcceleration = 16f;
    public float runningTopSpeed = 7.5f;
    public float runningTurningRrag = 14f;
}
