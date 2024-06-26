using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Game : Singleton<Game>
{
    public UnityEvent<int> OnRetriesSet;
    public UnityEvent OnSavingRequested;
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

    public virtual LevelData[] LevelsData()
    {
        return levels.Select(level => level.ToData()).ToArray();
    }
    
    public virtual GameLevel GetCurrentLevel()
    {
        var scene = GameLoader.instance.currentScene;
        return levels.Find((level) => level.scene == scene);
    }
    
    public virtual int GetCurrentLevelIndex()
    {
        var scene = GameLoader.instance.currentScene;
        return levels.FindIndex((level) => level.scene == scene);
    }
    
    public virtual void RequestSaving()
    {
        GameSaver.instance.Save(ToData(), m_dataIndex);
        OnSavingRequested?.Invoke();
    }
    
    /// <summary>
    /// Unlocks the next level from the levels list.
    /// </summary>
    public virtual void UnlockNextLevel()
    {
        var index = GetCurrentLevelIndex() + 1;

        if (index >= 0 && index < levels.Count)
        {
            levels[index].locked = false;
        }
    }

    /// <summary>
    /// Returns the Game Data of this Game to be used by the Data Layer.
    /// </summary>
    public virtual GameData ToData()
    {
        return new GameData()
        {
            retries = m_retries,
            levels = LevelsData(),
            createAt = m_createdAt.ToString(),
            updateAt = DateTime.UtcNow.ToString()
        };
    }
    
    protected override void Awake()
    {
        base.Awake();
        retries = initialRetries;
        DontDestroyOnLoad(gameObject);
    }
}
