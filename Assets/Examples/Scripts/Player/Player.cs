using System;
using Unity.Jobs;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerStatsManager))]
[RequireComponent(typeof(PlayerStateManager))]
[RequireComponent(typeof(Health))]
public class Player : Entity<Player>
{
    public PlayerEvents playerEvents;
    public Transform pickableSlot;
    public Transform skin;

    protected Vector3 m_respawnPosition;
    protected Quaternion m_respawnRotation;

    protected Vector3 m_skinInitialPosition;
    protected Quaternion m_skinInitialRotation;
    public PlayerInputManager inputs { get; protected set; }
    public PlayerStatsManager stats { get; protected set; }
    public int jumpCounter { get; protected set; }
    public bool holding { get; protected set; }
    public Health health { get; protected set; }
    public bool onWater { get; protected set; }
    public virtual void FaceDirectionSmooth(Vector3 direction) =>
        FaceDirectionSmooth(direction, stats.current.rotationSpeed);

    public virtual void Decelerate() => Decelerate(stats.current.deceleration);
    protected virtual void InitializeInputs() => inputs = GetComponent<PlayerInputManager>();
    protected virtual void InitializeStats() => stats = GetComponent<PlayerStatsManager>();
    protected virtual void InitializeTag() => tag = GameTags.Player;
    protected virtual void InitializeHealth() => health = GetComponent<Health>();
    protected virtual void InitializeRespawn()
    {
        m_respawnPosition = transform.position;
        m_respawnRotation = transform.rotation;
    }
	public virtual void SnapToGround() => SnapToGround(stats.current.snapForce);
    protected override void Awake()
    {
        base.Awake();
        InitializeInputs(); 
        InitializeStats();
        InitializeHealth();
        InitializeTag();
        InitializeRespawn();
    }

    public virtual void Accelerate(Vector3 direction)
    {
        var turningDrag = isGrounded && inputs.GetRun() 
            ? stats.current.runningTurningDrag 
            : stats.current.turningDrag;
        
        var acceleration = isGrounded && inputs.GetRun() 
            ? stats.current.runningAcceleration
            : stats.current.acceleration;
        
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
     public virtual void SnapToGround(float force)
     {
         if (isGrounded && (verticalVelocity.y <= 0))
         {
             verticalVelocity = Vector3.down * force;
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
     
     /// <summary>
     /// Resets Player state, health, position, and rotation.
     /// </summary>
     public virtual void Respawn()
     {
         health.Reset();
         transform.SetPositionAndRotation(m_respawnPosition, m_respawnRotation);
         states.Change<IdlePlayerState>();
     }
     
     public override void ApplyDamage(int amount, Vector3 origin)
     {
         if (!health.isEmpty && !health.recovering)
         {
             health.Damage(amount);
             var damageDir = origin - transform.position;
             damageDir.y = 0;
             damageDir = damageDir.normalized;
             FaceDirection(damageDir);
             lateralVelocity = -transform.forward * stats.current.hurtBackwardsForce;

             if (!onWater)
             {
                 verticalVelocity = Vector3.up * stats.current.hurtUpwardForce;
                 states.Change<HurtPlayerState>();
             }

             playerEvents.OnHurt?.Invoke();

             if (health.isEmpty)
             {
                 Throw();
                 playerEvents.OnDie?.Invoke();
             }
         }
     }
     
     public virtual void Throw()
     {
         if (holding)
         {
             var force = lateralVelocity.magnitude * stats.current.throwVelocityMultiplier;
             //pickable.Release(transform.forward, force);
             //pickable = null;
             holding = false;
             playerEvents.OnThrow?.Invoke();
         }
     }
}