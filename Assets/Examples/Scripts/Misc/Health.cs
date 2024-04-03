using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int initial = 3;
    public int max = 3;
    public UnityEvent onChange;
    protected int m_currentHealth;
    public int current
    {
        get { return m_currentHealth; }
        protected set
        {
            var last = m_currentHealth;

            if (value != last)
            {
                m_currentHealth = Mathf.Clamp(value, 0, max);
                onChange?.Invoke();
            }
        }
    }

    public virtual void Reset()
    {
        current = initial;
    }

    protected void Awake()
    {
        current = initial;
    }
}
