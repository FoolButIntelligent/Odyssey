using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInputManager : MonoBehaviour
{
    public InputActionAsset actions;

    protected InputAction m_movement;

    protected virtual void Awake() => CacheActions();
    // Start is called before the first frame update
    void Start()
    {
        actions.Enable();
    }

    protected virtual void Update()
    {
        
    }

    protected void OnEnable()
    {
        actions?.Enable();
    }

    protected void OnDisable()
    {
        actions?.Disable();
    }
    protected virtual void CacheActions()
    {
        m_movement = actions["Movement"];
    }
}
