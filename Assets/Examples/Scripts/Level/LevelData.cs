using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData : MonoBehaviour
{
    public bool locked;
    public int coins;
    public float time;
    public bool[] stars = new bool[GameLevel.StarsPerLevel];
}
