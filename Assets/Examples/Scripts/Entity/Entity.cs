using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    
}
public abstract class Entity<T> : Entity where T :Entity<T>
{
    public EntityStateManager<T> states { get; protected set; }
    protected virtual void HandleStates() => states.Step();
    protected virtual void InitializeStateManager() => states = GetComponent<EntityStateManager<T>>();
    public bool isGrounded { get; protected set; } = true;
    public Vector3 velocity { get; set; }
    public float turningDragMultiplier { get; set; } = 1f;
    public float turningSpeedMultiplier { get; set; } = 1f;
    public float accelerationMultiplier { get; set; } = 1f;
    public Vector3 lateralVelocity
    {
        get { return new Vector3(velocity.x, 0, velocity.z);}
        set { velocity = new Vector3(value.x, velocity.y, value.z); }
    }
    protected virtual void Awake()
    {
       InitializeStateManager();
    }

    protected void Update()
    {
        HandleStates();
        HandleController();
    }

    protected virtual void HandleController()
    {
        transform.position += velocity * Time.deltaTime;
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