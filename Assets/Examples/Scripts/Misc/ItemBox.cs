using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemBox : MonoBehaviour,IEntityContact
{
    protected BoxCollider m_collider;
    protected Vector3 m_initialScale;
    public Collectable[] collectables;
    protected virtual void InitializeCollectables()
    {
        foreach (var collectable in collectables)
        {
            
        }
    }
    
    protected void Start()
    {
        m_collider = GetComponent<BoxCollider>();
        m_initialScale = transform.localScale;
        InitializeCollectables();
    }
}
