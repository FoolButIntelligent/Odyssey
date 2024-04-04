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
    public float pushForce = 4f;
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
    [Header("Jump Stats")] 
    public int multiJumps = 1;
    public float coyoteJumpThreshold = 0.15f;
    public float maxJumpHeight = 17f;
    public float minJumpHeight = 10f;
    [Header("Stomp Attack Stats")] 
    public bool canStompAttack = true;
    public float stompAirTime = 0.8f;
    public float stompDownwardForce = 20f;
    public float stompGroundTime = 0.5f;
    public float stompGroundLeapHeight = 10f;
}

