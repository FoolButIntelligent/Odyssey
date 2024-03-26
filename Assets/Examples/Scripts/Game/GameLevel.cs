using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameLevel  
{
   public bool locked;
   public string scene;
   public string name;
   public string description;
   public Sprite image;
   
   public int coins { get; set; }
   public float time { get; set; }
   public static readonly int StarsPerLevel = 3;
   public bool[] stars { get; set; } = new bool[StarsPerLevel];
}