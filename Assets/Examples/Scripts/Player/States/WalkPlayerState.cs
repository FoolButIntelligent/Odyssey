using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPlayerState : PlayerState 
{
   protected override void OnEnter(Player player)
   {
      
   }

   protected override void OnExit(Player player)
   {
      
   }

   protected override void OnStep(Player player)
   {
      var inputDirection = player.inputs.GetMovementCameraDirection();

      if (inputDirection.sqrMagnitude > 0 )
      {
         var dot = Vector3.Dot(inputDirection, player.lateralVelocity);
         if(dot >= player.stats.current.brakeThreshold)
            player.Accelerate(inputDirection);
      }
   }
}