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
}
