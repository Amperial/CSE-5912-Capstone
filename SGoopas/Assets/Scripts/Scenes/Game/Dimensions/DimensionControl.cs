using System;
using UnityEngine;

public class DimensionControl : MonoBehaviour {

    public static DimensionControl Instance;

    public void Awake() {
        Instance = this;
    }

    private Dimension currentDimension = Dimension.THREE_D;
    public Dimension CurrentDimension {
        get {
            return currentDimension;
        }
    }
    
    public static event Action<Dimension, Cancellable> OnSwitchDimension;

    public void SwitchDimension() {
        // Switch to opposite dimension
        SetDimension(currentDimension == Dimension.TWO_D ? Dimension.THREE_D : Dimension.TWO_D);
    }

    public void SetDimension(Dimension dimension) {
        // Avoid trying to switch to the current dimension
        if (dimension != currentDimension) {
            // Call dimension switch event with new cancellable
            Cancellable cancellable = new Cancellable();
            OnSwitchDimension(dimension, cancellable);

            // Only update dimension if the switch wasn't cancelled
            if (!cancellable.IsCancelled) {
                currentDimension = dimension;
            }
        }
    }

}
