using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    protected List<IObserver> myObservers = new List<IObserver>();
    protected int numOberservers;

    public void AddOberserver(IObserver observer)
    {
        myObservers.Add(observer);
        numOberservers++;
    }

    public void RemoveObserver(IObserver observer)
    {
        myObservers.Remove(observer);
        numOberservers--;
    }

    public void ClearObservers()
    {
        myObservers.Clear();
    }

    protected void Notify(MyEvent _event)
    {
        foreach(IObserver o in myObservers)
        {
            o.onNotify(_event);
        }
    }
}
