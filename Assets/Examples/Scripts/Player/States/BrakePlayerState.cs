using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakePlayerState : PlayerState
{
    protected override void OnEnter(Player player)
    {
      
    }

    protected override void OnExit(Player player)
    {
      
    }

    protected override void OnStep(Player player)
    {
        player.Jump();
        player.Fall();
        player.Decelerate();
        
        if (player.lateralVelocity.sqrMagnitude == 0) 
        { 
            player.states.Change<IdlePlayerState>();
        }
    }
}
