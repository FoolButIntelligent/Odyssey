using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats<PlayerStats>
{ 
    [Header("Motion Stats")] 
    public float brakeThreshold = -0.8f;
    public float turningDrag = 28f;
    public float acceleraiton = 13f;
    public float topSpeed = 6f;
    public float airAcceleration = 32f;

    [Header("Running Stats")] 
    public float runningAcceleration = 16f;
    public float runningTopSpeed = 7.5f;
    public float runningTurningRrag = 14f;
}
