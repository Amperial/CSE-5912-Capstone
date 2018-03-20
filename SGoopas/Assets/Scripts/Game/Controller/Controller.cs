using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Controller {
    Dictionary<string, Action> buttonDowns;
    Dictionary<string, Tuple<Action, Action>> axis;
    public Controller()
    {
        buttonDowns = new Dictionary<string, Action>();
        axis = new Dictionary<string, Tuple<Action, Action>>();
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
        foreach (KeyValuePair<string, Action> entry in buttonDowns)
        {
            if (Input.GetButtonDown(entry.Key))
            {
                entry.Value();
            }
        }
        foreach (KeyValuePair<string, Tuple<Action, Action>> entry in axis)
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

    public void FixedUpdate()
    {
        
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
