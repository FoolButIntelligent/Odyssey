using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStateManager :EntityStateManager<Player>
{
    [ClassTypeName(typeof(PlayerState))] 
    public string[] states;

    protected override List<EntityState<Player>> GetStateList()
    {
        return PlayerState.CreatListFromStringArray(states);
    }
}
