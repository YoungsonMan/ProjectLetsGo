using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler
{
    public event Action<Collider> OnTriggerExitEvent;
    public event Action<Collider> OnTriggerEnterEvent;
    
    public void NotifyTriggerExit(Collider collision)
    {
        OnTriggerExitEvent?.Invoke(collision);
    }

    public void NotifyTriggerEnter(Collider collision)
    {
        OnTriggerEnterEvent?.Invoke(collision);
    }
}
