using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : Singleton<Game>
{
    public UnityEvent<int> OnRetriesSet;
    public List<GameLevel> levels;
    public int initialRetries = 3;
    protected int m_dataIndex;
    protected int m_retries;
    protected DateTime m_createdAt;
    protected DateTime m_updatedAt;
    
    public int retries
    {
        get { return m_retries; }
        set
        {
            m_retries = value;
            OnRetriesSet?.Invoke(m_retries);
        }
    }

    public virtual void LoadState(int index, GameData data)
    {
        m_dataIndex = index;
        m_retries = data.retries;
        m_createdAt = DateTime.Parse(data.createAt);
        m_updatedAt = DateTime.Parse(data.updateAt);

        for (int i = 0; i < data.levels.Length; i++)
        {
            levels[i].LoadState(data.levels[i]);
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        retries = initialRetries;
        DontDestroyOnLoad(gameObject);
    }
}
