using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelPause : MonoBehaviour
{
   protected Player m_player;
   protected LevelPauser m_pauser;
   
   protected void Start()
   {
      m_player = GetComponent<Player>();
      m_pauser = LevelPauser.instance;
   }

   protected void Update()
   {
      if (m_player.inputs.GetPauseDown())
      {
         var value = m_pauser.paused;
         m_pauser.Pause(!value);
      }
   }
}