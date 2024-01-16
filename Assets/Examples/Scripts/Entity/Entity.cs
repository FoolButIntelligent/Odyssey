using UnityEngine;
public abstract class Entity<T> : Entity where T :Entity<T> 
{
    
}