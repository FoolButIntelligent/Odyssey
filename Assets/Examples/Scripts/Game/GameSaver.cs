using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using OpenCover.Framework.Model;
using TMPro;
using UnityEngine;
using File = System.IO.File;

public class GameSaver : Singleton<GameSaver>
{
   protected static readonly int TotolSlots = 5;
   public enum Mode
   {
      Binary,JSON,PlayerPrefs
   }

   public Mode mode = Mode.Binary;
   public string binaryFileExtension = "data";
   public string fileName = "save";
   public virtual GameData[] LoadList()
   {
      var list = new GameData[TotolSlots];
      for (int i = 0; i < TotolSlots; i++)
      {
         var data = Load(i); 

         if (data != null)
         {
            list[i] = data;
         }
         
      }
      return list;
   }

   public virtual GameData Load(int index)
   {
      switch (mode)
      {
         default:
         case Mode.Binary:
            return LoadBinary(index);
         case Mode.JSON:
            return LoadJson(index);
         case Mode.PlayerPrefs:
            return LoadPlayerPrefs(index);
      }
   }

   protected virtual void SaveBinary(GameData data, int index)
   {
      var path = GetFilePath(index);
      var formmatter = new BinaryFormatter();
      var stream = new FileStream(path, FileMode.Open);
      formmatter.Serialize(stream,data);
      stream.Close();
   }
   
   protected virtual GameData LoadBinary(int index)
   {
      var path = GetFilePath(index);

      if (File.Exists(path))
      {
         var formatter = new BinaryFormatter();
         var stream = new FileStream(path, FileMode.Open);
         var data = formatter.Deserialize(stream);
         stream.Close();
         return data as GameData;
      }

      return null;
   }
   
   protected virtual void SaveJson(GameData data, int index)
   {
      var json = data.ToJson();
      var path = GetFilePath(index);
      File.WriteAllText(path,json);
   }
   
   protected virtual GameData LoadJson(int index)
   {
      var path = GetFilePath(index);

      if (File.Exists(path))
      {
         var json = File.ReadAllText(path);
         
         return GameData.FromJson(json);
      }

      return null;
   }
   protected virtual void SavePlayerPrefs(GameData data, int index)
   {
      var json = data.ToJson();
      var key = index.ToString();
      PlayerPrefs.SetString(key,json);
   }
   protected virtual GameData LoadPlayerPrefs(int index)
   {
      var key = index.ToString();

      if (PlayerPrefs.HasKey(key))
      {
         var json = PlayerPrefs.GetString(key);
         return GameData.FromJson(json);
      }

      return null;
   }
   
   protected virtual string GetFilePath(int index)
   {
      var extension = mode == Mode.JSON ? "Json" : binaryFileExtension;
      return Application.persistentDataPath + $"/{fileName}_{index}.{extension}";
   }
}
