using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IStateEventDelegate {
    bool CurrentState();
    UnityEngine.Object StateObject();
}

public class Controller {
    Dictionary<string, Action> buttonDowns;
    Dictionary<string, Tuple<Action, Action>> axis;
    Dictionary<IStateEventDelegate, Tuple<bool, Action<bool, UnityEngine.Object>>> events;
	public Controller()
    {
        buttonDowns = new Dictionary<string, Action>();
        axis = new Dictionary<string, Tuple<Action, Action>>();
        events = new Dictionary<IStateEventDelegate, Tuple<bool, Action<bool, UnityEngine.Object>>>();
    }

    /*
     * Watches a state value for change.
     */
    public void RegisterStateEvent(IStateEventDelegate eventDelegate, Action<bool, UnityEngine.Object> action) {
        Tuple<bool, Action<bool, UnityEngine.Object>> stateAction = new Tuple<bool, Action<bool, UnityEngine.Object>>(eventDelegate.CurrentState(), action);
        if (events.ContainsKey(eventDelegate))
            events.Remove(eventDelegate);
        events.Add(eventDelegate, stateAction);
    }
 
    public void RegisterAxis(string axisName, Action negativeAction, Action positiveAction)
    {
        Tuple<Action,Action> action = new Tuple<Action, Action>(negativeAction, positiveAction);
        if (axis.ContainsKey(axisName))
            axis.Remove(axisName);
        axis.Add(axisName, action);
    }

    public void RegisterButtonDown(string buttonName, Action action)
    {
        if (buttonDowns.ContainsKey(buttonName))
            buttonDowns.Remove(buttonName);
        buttonDowns.Add(buttonName, action);
    }

    public void Update()
    {
        foreach (KeyValuePair<IStateEventDelegate, Tuple<bool, Action<bool, UnityEngine.Object>>> entry in events)
        {
            Tuple<bool, Action<bool, UnityEngine.Object>> stateEvent = entry.Value;
            IStateEventDelegate eventDelegate = entry.Key;
            if (eventDelegate.CurrentState() != stateEvent.Item1)
            {
                stateEvent.Item1 = eventDelegate.CurrentState();
                stateEvent.Item2(eventDelegate.CurrentState(), eventDelegate.StateObject());
            }
        }
        foreach (KeyValuePair<string, Action> entry in buttonDowns)
        {
            if (Input.GetButtonDown(entry.Key))
            {
                entry.Value();
            }
        }
        foreach (KeyValuePair<string, Tuple<Action,Action>> entry in axis)
        {
            float axisValue = Input.GetAxis(entry.Key);
            if (axisValue > 0)
            {
                entry.Value.Item2();
            }
            else if (axisValue < 0)
            {
                entry.Value.Item1();
            }
        }
    }

    /** If unity ever upgrades their .NET framework, we can use the tuples detailed here:
      * https://msdn.microsoft.com/en-us/library/system.tuple(v=vs.110).aspx
    */
    private class Tuple<T1, T2>
    {
        public T1 Item1;
        public T2 Item2;

        public Tuple(T1 t1, T2 t2)
        {
            Item1 = t1;
            Item2 = t2;
        }

    }
}
