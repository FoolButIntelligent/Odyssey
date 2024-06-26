using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelScore : Singleton<LevelScore>
{
    public bool stopTime { get; set; } = true;
    protected int m_coins;
    
    protected Game m_game;
    protected GameLevel m_level;
    protected bool[] m_stars = new bool[GameLevel.StarsPerLevel];
    public UnityEvent<int> OnCoinsSet;
    public UnityEvent<bool[]> OnStarsSet;
    public UnityEvent OnScoreLoded;
    
    public int coins
    {
        get { return m_coins; }
        set
        {
            m_coins = value;
            OnCoinsSet?.Invoke(m_coins);
        }
    }

    public bool[] stars => (bool[])m_stars.Clone();
    
    public float time { get; protected set; }

    public virtual void Consolidate()
    {
        if (m_level != null)
        {
            if (m_level.time == 0 || time < m_level.time)
            {
                m_level.time = time;
            }

            if (coins > m_level.coins)
            {
                m_level.coins = coins;
            }

            m_level.stars = (bool[])stars.Clone();
            m_game.RequestSaving();
        }
    }
    
    protected void Start()
    {
        m_game = Game.instance;
        m_level = m_game?.GetCurrentLevel();

        if (m_level != null)
        {
            m_stars = (bool[])m_level.stars.Clone();
        }
        
        OnScoreLoded?.Invoke();
    }

    protected virtual void Update()
    {
        if (!stopTime)
        {
            time += Time.deltaTime;
        }
    }
}
