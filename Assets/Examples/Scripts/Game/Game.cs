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

    public static void LockCursor(bool value = true)
    {
#if UNITY_STANDALONE//确保该代码仅在 Unity 应用程序的独立构建中被编译执行
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
#endif
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
