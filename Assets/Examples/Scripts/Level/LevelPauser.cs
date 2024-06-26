using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPauser : Singleton<LevelPauser>
{
   public bool paused { get; protected set; }
   public bool canPause { get; set; }
   public UIAnimator pauseScreen;
   public UnityEvent OnPause;
   public UnityEvent OnUnPause;
   
   public virtual void Pause(bool value)
   {
      if (paused != value)
      {
         if (!paused)
         {
            Game.LockCursor(false);
            paused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            pauseScreen?.Show();
            OnPause?.Invoke();
         }
         else
         {
            Game.LockCursor(true);
            paused = false;
            Time.timeScale = 1;
            pauseScreen?.Hide();
            OnUnPause?.Invoke();
         }
      }
   }
}
