using System.Collections.Generic;

public class Cancellable {

    private List<System.Action> callbacks = new List<System.Action>();
    private bool cancelled = false;

    public bool IsCancelled {
        get {
            return cancelled;
        }
    }

    // Alternate PerformCancellable method for simplified use in specifying action and callback when switching dimensions.
    public void PerformCancellable(Dimension target, System.Action to2D, System.Action to3D) {
        if (target == Dimension.TWO_D) {
            PerformCancellable(to2D, to3D);
        } else if (target == Dimension.THREE_D) {
            PerformCancellable(to3D, to2D);
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

    // Cancels the cancellable if a condition evaluates to true
    public void CancelIf(System.Func<bool> condition) {
        if (!IsCancelled && condition()) {
            Cancel();
        }
    }

}
