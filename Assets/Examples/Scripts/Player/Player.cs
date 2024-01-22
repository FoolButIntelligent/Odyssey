using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Player : Entity<Player>
{
    public PlayerEvents PlayerEvents;
    public PlayerInputManager inputs { get; protected set; }
    public PlayerStatsManager stats { get; protected set; }
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

    public virtual void Accelerate(Vector3 direction, float turningDrag, float acceleration, float topSpeed)
    {
        if (direction.sqrMagnitude >= 0)
        {
            var speed = Vector3.Dot(direction, lateralVelocity);
            var velocity = direction * speed;
            var turningVelocity = lateralVelocity - velocity;
            var turningDelta = turningDrag * turningDragMultiplier * Time.deltaTime;
            var targetTopSpeed = topSpeed * turningSpeedMultiplier;

            if (lateralVelocity.magnitude < targetTopSpeed || speed < 0)
            {
                speed += acceleration * accelerationMultiplier*Time.deltaTime;
                speed = Mathf.Clamp(speed, -targetTopSpeed, targetTopSpeed);
            }

            velocity = direction * speed;
            turningVelocity = Vector3.MoveTowards(turningVelocity, Vector3.zero, turningDelta);
            lateralVelocity = velocity + turningVelocity;
        }
    }
}