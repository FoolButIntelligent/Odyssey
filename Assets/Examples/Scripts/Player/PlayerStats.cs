using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats<PlayerStats>
{
    [Header("General Stats")] 
    public float rotationSpeed = 970f;
    public float friction = 16f;
    public float gravityTopSpeed = 58f;
    public float gravity = 38f;
    public float fallGravity = 65;
    [Header("Motion Stats")] 
    public float brakeThreshold = -0.8f;
    public float turningDrag = 28f;
    public float acceleraiton = 13f;
    public float topSpeed = 6f;
    public float airAcceleration = 32f;
    public float deceleration = 28f;

    [Header("Running Stats")] 
    public float runningAcceleration = 16f;
    public float runningTopSpeed = 7.5f;
    public float runningTurningRrag = 14f;
    [Header("Backflip Stats")]
    public bool canBackflip = true;
    public float backlflipJumpHeight = 23f;
    public float backflipGravity = 35f;
    public float backflipTuningDrag = 2.5f;
    public float backflipAirAcceleration = 12f;
    public float backflipTopSpeed = 7.5f;
    public float backflipBackwardTurnForce = 8f;
}

