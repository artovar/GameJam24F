using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEvent
{
    public Subject sender { get; }

    public EventType myType { get; }

    public enum EventType
    {
        baseType,
        damageDone,
        speedConsumed, invincibleConsumed
    }

    public MyEvent(Subject s, EventType t)
    {
        sender = s;
        myType = t;
    }
}
