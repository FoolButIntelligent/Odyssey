using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
   public int retries;
   public LevelData[] levels;
   public string creatAt;
   public string updateAt;

   public static GameData Create()
   {
      return new GameData()
      {
         retries = Game.instance.initialRetries,
         creatAt = DateTime.UtcNow.ToString(),
         updateAt = DateTime.UtcNow.ToString()
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
