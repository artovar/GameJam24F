using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObserversInitiator : MonoBehaviour
{

    List<Subject> subjects;
    List<IObserver> observers;

    private void Awake()
    {
        InitiateObservers();
    }

    private void InitiateObservers()
    {
        subjects = FindObjectsOfType<Subject>().ToList();
        observers = FindObjectsOfType<MonoBehaviour>().OfType<IObserver>().ToList();

        foreach (Subject s in subjects)
        {
            foreach (IObserver o in observers)
            {
                s.AddOberserver(o);
            }
        }
    }

    public void UpdateSubjects()
    {
        subjects = FindObjectsOfType<Subject>().ToList();
        observers = FindObjectsOfType<MonoBehaviour>().OfType<IObserver>().ToList();

        foreach (Subject s in subjects)
        {
            s.ClearObservers();
            foreach (IObserver o in observers)
            {
                s.AddOberserver(o);
            }
        }
    }
}
