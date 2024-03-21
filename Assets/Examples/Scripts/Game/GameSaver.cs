using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : Singleton<GameSaver>
{
   protected static readonly int TotolSlots = 5;
   public virtual GameData[] LoadList()
   {
      var list = new GameData[TotolSlots];
      for (int i = 0; i < TotolSlots; i++)
      {
         //var data = Load(i); TODO:Load function

         /*if (data != null)
         {
            list[i] = data;
         }*/
         
      }
      return list;
   }
}
