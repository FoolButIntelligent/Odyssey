using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

public class Player : Entity<Player>
{
    public PlayerEvents PlayerEvents;
    public PlayerInputManager inputs { get; protected set; }
    public PlayerStatsManager stats { get; protected set; }
    public int jumpCounter { get; protected set; }
    public bool holding { get; protected set; }

    public virtual void FaceDirectionSmooth(Vector3 direction) =>
        FaceDirectionSmooth(direction, stats.current.rotationSpeed);

    public virtual void Decelerate() => Decelerate(stats.current.deceleration);
    protected virtual void InitializeInputs() => inputs = GetComponent<PlayerInputManager>();
    protected virtual void InitializeStats() => stats = GetComponent<PlayerStatsManager>();

    protected override void Awake()
    {
        base.Awake();
        InitializeInputs(); 
        InitializeStats();
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
    
    public virtual void Backflip(float force)
    {
        if (stats.current.canBackflip)
        {
            verticalVelocity = Vector3.up * stats.current.backlflipJumpHeight;
            lateralVelocity = -transform.forward * force;
            states.Change<BackflipPlayerState>();
    //        PlayerEvents.OnBackflip.Invoke();
        }
    }
    
    public virtual void BackflipAcceleration()
    {
        var direction = inputs.GetMovementCameraDirection();
        Accelerate(direction,stats.current.backflipTuningDrag,stats.current.backflipAirAcceleration,stats.current.backflipTopSpeed);
    }
}