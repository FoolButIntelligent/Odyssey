using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInputManager : MonoBehaviour
{
    public InputActionAsset actions;
    protected float m_movementDirectionUnlockTime;
    protected InputAction m_movement;
    protected InputAction m_run;
    protected InputAction m_jump;
    protected Camera m_camera;
    protected float? m_lastJumpTime;
    protected const float k_jumpBuffer = 0.15f;
    
    protected virtual void Awake() => CacheActions();
  
    void Start()
    {
        m_camera = Camera.main;
        actions.Enable();
    }

    protected virtual void Update()
    {
        if (m_jump.WasPressedThisFrame())
        {
            m_lastJumpTime = Time.time;
        }
        
    }

    protected void OnEnable()
    {
        actions?.Enable();
    }

    protected void OnDisable()
    {
        actions?.Disable();
    }

    public virtual bool GetRun() => m_run.IsPressed();
    protected virtual void CacheActions()
    {
        m_movement = actions["Movement"];
        m_run = actions["Run"];
        m_jump = actions["Jump"];
    }

    public virtual Vector3 GetMovementDirection()
    {
        if(Time.time<m_movementDirectionUnlockTime) return Vector3.zero;
        var value = m_movement.ReadValue<Vector2>();
        return GetAxisWithCrossDeadZone(value);
    }

    public virtual Vector3 GetAxisWithCrossDeadZone(Vector2 axis)
    {
        var deadzone = InputSystem.settings.defaultDeadzoneMin;
        axis.x = Mathf.Abs(axis.x) > deadzone ? RemapToDeadZone(axis.x, deadzone) : 0;
        axis.y = Mathf.Abs(axis.y) > deadzone ? RemapToDeadZone(axis.y, deadzone) : 0;
        return new Vector3(axis.x, 0, axis.y);
    }

    protected float RemapToDeadZone(float value, float deadzone) => (value - deadzone) / (1 - deadzone);

    public virtual Vector3 GetMovementCameraDirection()
    {
        var direction = GetMovementDirection();
        if (direction.sqrMagnitude > 0)
        {
            var rotation = Quaternion.AngleAxis(m_camera.transform.eulerAngles.y, Vector3.up);
            direction = rotation * direction;
            direction = direction.normalized;
        }

        return direction;
    }

    public virtual bool GetJumpDown()
    {
        if (m_lastJumpTime != null && Time.time - m_lastJumpTime < k_jumpBuffer)
        {
            m_lastJumpTime = null;
            return true;
        }
        return false;
    }

    public virtual bool GetJumpUp() => m_jump.WasReleasedThisFrame();
}














