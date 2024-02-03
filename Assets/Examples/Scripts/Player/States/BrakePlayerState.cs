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
        player.Decelerate();
        // var inputDerection = player.inputs.GetMovementCameraDirection();
        // //
        // // if (player.stats.current.canBackflip &&
        // //     Vector3.Dot(inputDerection, player.transform.forward) < 0)
        // // {
        // //     // player.Backflip(player.stats.current.backflipBackwardTurnForce);
        // // }
        // // else
        // // {
            if (player.lateralVelocity.sqrMagnitude == 0)
            {
                player.states.Change<IdlePlayerState>();
            }
        // }
    }
}
