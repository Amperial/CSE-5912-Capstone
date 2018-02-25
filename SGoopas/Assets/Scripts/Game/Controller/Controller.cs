using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Controller {
    Dictionary<string, Action> buttons;
    Dictionary<string, Tuple<Action, Action>> axis;
	public Controller()
    {
        buttons = new Dictionary<string, Action>();
        axis = new Dictionary<string, Tuple<Action, Action>>();
    }
 
    public void RegisterAxis(string axisName, Action negativeAction, Action positiveAction)
    {
        Tuple<Action,Action> action = new Tuple<Action, Action>(negativeAction, positiveAction);
        axis.Add(axisName, action);
    }

    public void RegisterButton(string axisName, Action action)
    {
        buttons.Add(axisName, action);
    }

    public void Update()
    {
        foreach (KeyValuePair<string, Action> entry in buttons)
        {
            if (Input.GetButton(entry.Key))
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
