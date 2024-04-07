using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerEvents
{
    public UnityEvent OnJump;
    public UnityEvent OnHurt;
    public UnityEvent OnDie;
    public UnityEvent OnSpin;
    public UnityEvent OnPickUp;
    public UnityEvent OnThrow;
    public UnityEvent OnStompStartd;
    public UnityEvent OnStompFalling;
    public UnityEvent OnStompLanding;
    public UnityEvent OnStompEnding;
    public UnityEvent OnLedgeGrabbed;
    public UnityEvent OnLedgeClimbing;
    public UnityEvent OnAirDive;
    public UnityEvent OnBackflip;
    public UnityEvent OnGlidingStarted;
    public UnityEvent OnGlidingStoped;
    public UnityEvent OnDashStarted;
    public UnityEvent OnDashEnded;

}  