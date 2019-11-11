﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    private class TimedEvent
    {
        public float timeToExecute;
        public Callback Method;
    }

    public delegate void Callback();


    private List<TimedEvent> events;
    void Awake()
    {
        events = new List<TimedEvent>();
    }
    public void Add(Callback method, float inSeconds)
    {

        events.Add(new TimedEvent
        {
            Method = method,
            timeToExecute = Time.time + inSeconds
        });
    }
    public void delete(Callback method)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].Method.Method.Name == method.Method.Name)
            {
                events.Remove(events[i]);
                return;
            }
        }
    }
    public void Print()
    {
        for (int i = 0; i < events.Count; i++)
        {
            print(events[i].Method.Method);
        }
    }
    void Update()
    {
        if (events.Count == 0)
            return;

        for (int i = 0; i < events.Count; i++)
        {
            var timedEvent = events[i];
            if (timedEvent.timeToExecute <= Time.time)
            {
                timedEvent.Method();
                events.Remove(timedEvent);
            }
        }
    }
}
