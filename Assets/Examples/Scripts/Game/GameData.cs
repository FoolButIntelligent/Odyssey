using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData 
{
   public int retries;
   public LevelData[] levels;
   public string createAt;
   public string updateAt;

   public static GameData Create()
   {
      return new GameData()
      {
         retries = Game.instance.initialRetries,
         createAt = DateTime.UtcNow.ToString(),
         updateAt = DateTime.UtcNow.ToString(),
         levels = Game.instance.levels.Select((level) =>
         { 
            return new LevelData()
            {
               locked = level.locked
            };
         }).ToArray()
      };
   }
   public virtual string ToJson()
   {
      return JsonUtility.ToJson(this);
   }
   
   public static GameData FromJson(string json)
   {
      return JsonUtility.FromJson<GameData>(json);
   }
}
