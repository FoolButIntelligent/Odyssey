using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LevelFinisher : Singleton<LevelFinisher>
{
    public float loadingDelay = 1f;
    protected LevelPauser m_pause => LevelPauser.instance;
    protected Level m_level => Level.instance;
    protected GameLoder m_loder => GameLoder.instance;
    public string exitScene;

    public UnityEvent OnExit;
    
    protected virtual IEnumerator ExitRoutine()
    {
        m_pause.Pause(false);
        m_pause.canPause = false;
        m_level.player.inputs.enabled = false;
        yield return new WaitForSeconds(loadingDelay);
        Game.LockCursor(false);
        m_loder.Load(exitScene);
        OnExit?.Invoke();
    }
    
    public virtual void Exit()
    {
        StopAllCoroutines();
        StartCoroutine(ExitRoutine());
    }
}
