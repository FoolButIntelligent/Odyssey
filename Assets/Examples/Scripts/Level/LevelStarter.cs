using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStarter : Singleton<LevelStarter>
{
    public UnityEvent OnStart;
    protected Level m_level => Level.instance;
    protected LevelScore m_score => LevelScore.instance;
    protected LevelPauser m_pause => LevelPauser.instance;
    public float enablePLayerDelay = 1f;
    
    protected virtual IEnumerator Routinue()
    {
        Game.LockCursor();
        m_level.player.controller.enabled = false;
        m_level.player.inputs.enabled = false;
        yield return new WaitForSeconds(enablePLayerDelay);
        m_score.stopTime = false;
        m_level.player.controller.enabled = true;
        m_level.player.inputs.enabled = true;
        m_pause.canPause = true;
        OnStart?.Invoke();
    }
        
    protected void Start()
    {
        StartCoroutine(Routinue());
    }
}
