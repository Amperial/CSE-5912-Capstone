using System.Collections.Generic;

public class Cancellable {

    private bool cancelled = false;
    private List<System.Action> callbacks = new List<System.Action>();

    // Check if the cancellable is cancelled
    public bool IsCancelled() {
        return cancelled;
    }

    // Invoke the given callback if the cancellable isn't cancelled
    public void Perform(System.Action callback) {
        if (!IsCancelled()) {
            callback();
        }
    }

    // Register a callback to be invoked if/when the cancellable is cancelled
    public void OnCancel(System.Action callback) {
        if (!IsCancelled()) {
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
