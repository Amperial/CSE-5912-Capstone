using System.Collections.Generic;

public class Cancellable {

    private bool cancelled = false;
    private List<System.Action> callbacks = new List<System.Action>();

    // Check if the cancellable is cancelled
    public bool IsCancelled()
    {
        return cancelled;
    }

    // Register a callback to be invoked if the Cancellable is cancelled
    public void IfCancelled(System.Action callback)
    {
        if (IsCancelled())
        {
            // Already cancelled, invoke callback
            callback();
        } else
        {
            // Register callback
            callbacks.Add(callback);
        }
    }

    // Cancel the cancellable, invoke callbacks
    public virtual void Cancel()
    {
        cancelled = true;
        foreach (System.Action callback in callbacks)
        {
            callback();
        }
        callbacks.Clear();
    }

}
