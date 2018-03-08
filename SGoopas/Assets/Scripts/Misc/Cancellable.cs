using System.Collections.Generic;

public class Cancellable {

    private List<System.Action> callbacks = new List<System.Action>();
    private bool cancelled = false;

    public bool IsCancelled {
        get {
            return cancelled;
        }
    }

    // Invoke the given action if the cancellable hasn't cancelled, and register OnCancel callback.
    // Essentially combines Perform(action) and OnCancel(callback) methods into one.
    public void PerformCancellable(System.Action action, System.Action callback) {
        if (!IsCancelled) {
            // Invoke action
            action();
            // Register callback
            callbacks.Add(callback);
        }
    }

    // Invoke the given action if the cancellable hasn't been cancelled
    public void Perform(System.Action action) {
        if (!IsCancelled) {
            // Invoke action
            action();
        }
    }

    // Register a callback to be invoked if/when the cancellable is cancelled
    public void OnCancel(System.Action callback) {
        if (!IsCancelled) {
            // Register callback
            callbacks.Add(callback);
        }
    }

    // Cancel the cancellable, invoke callbacks
    public virtual void Cancel() {
        cancelled = true;
        foreach (System.Action callback in callbacks) {
            callback();
        }
        callbacks.Clear();
    }

}
