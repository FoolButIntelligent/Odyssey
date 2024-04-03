﻿using System;
using Unity.Jobs;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

public class Player : Entity<Player>
{
    public PlayerEvents playerEvents;
    public PlayerInputManager inputs { get; protected set; }
    public PlayerStatsManager stats { get; protected set; }
    public int jumpCounter { get; protected set; }
    public bool holding { get; protected set; }
    public Health health { get; protected set; }

    public virtual void FaceDirectionSmooth(Vector3 direction) =>
        FaceDirectionSmooth(direction, stats.current.rotationSpeed);

    public virtual void Decelerate() => Decelerate(stats.current.deceleration);
    protected virtual void InitializeInputs() => inputs = GetComponent<PlayerInputManager>();
    protected virtual void InitializeStats() => stats = GetComponent<PlayerStatsManager>();
    protected virtual void InitializeTag() => tag = GameTags.Player;
    protected virtual void InitializeHealth() => health = GetComponent<Health>();
    
    protected override void Awake()
    {
        base.Awake();
        InitializeInputs(); 
        InitializeStats();
        InitializeHealth();
        InitializeTag();
    }

    public virtual void Accelerate(Vector3 direction)
    {
        var turningDrag = isGrounded && inputs.GetRun() 
            ? stats.current.runningTurningRrag 
            : stats.current.turningDrag;
        
        var acceleration = isGrounded && inputs.GetRun() 
            ? stats.current.runningAcceleration
            : stats.current.acceleraiton;
        
        var topSpeed = inputs.GetRun() 
            ? stats.current.runningTopSpeed
            : stats.current.topSpeed;

        var finalAcceleration = isGrounded ? acceleration : stats.current.airAcceleration;
        Accelerate(direction,turningDrag,finalAcceleration,topSpeed);
    }

    public virtual void Friction()
    {
        Decelerate(stats.current.friction);
    }
    
     public virtual void Gravity()
     {
         if (!isGrounded && verticalVelocity.y > -stats.current.gravityTopSpeed)
         {
             var speed = verticalVelocity.y;
             var force = verticalVelocity.y > 0 ? stats.current.gravity : stats.current.fallGravity;
             speed -= force * gravityMultiplier * Time.deltaTime;
             speed = Mathf.Max(speed, -stats.current.gravityTopSpeed);
             verticalVelocity = new Vector3(0, speed, 0);
             
         }
         
     }

     public virtual void Fall()
     {
         if (!isGrounded)
         {
             states.Change<FallPlayerState>();
         }
     }

     public virtual void Jump()
     {
         var canMultiJump = (jumpCounter > 0) && (jumpCounter < stats.current.multiJumps);
         var canCoyoteJump = (jumpCounter == 0) && (Time.time < lastGroundTime + stats.current.coyoteJumpThreshold);

         if (isGrounded || canMultiJump || canCoyoteJump)
         {
             if (inputs.GetJumpDown())
             {
                 Jump(stats.current.maxJumpHeight);
             }
         }

         if (inputs.GetJumpUp() && (jumpCounter > 0) && verticalVelocity.y > stats.current.minJumpHeight)
         {
             verticalVelocity = Vector3.up * stats.current.minJumpHeight;
         }
     }

     public virtual void Jump(float height)
     {
         jumpCounter++;
         verticalVelocity = Vector3.up * height;
         states.Change<FallPlayerState>();
         playerEvents.OnJump?.Invoke();
     }

     public virtual void PushRigidbody(Collider other)
     {
         if (IsPointUnderStep(other.bounds.max) && other.TryGetComponent(out Rigidbody rigidbody))
         {
             var force = lateralVelocity * stats.current.pushForce;
             rigidbody.velocity += force / rigidbody.mass * Time.deltaTime;
         }
     }
}