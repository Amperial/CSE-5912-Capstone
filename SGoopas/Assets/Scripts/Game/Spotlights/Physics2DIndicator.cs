using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2DIndicator : MonoBehaviour
{

    private bool canSwitch;
    private bool death;

    // Use this for initialization
    void Start()
    {
        canSwitch = true;
        death = false;
    }

    void LateUpdate()
    {
        if (death)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == MainObjectContainer.Instance.Player2D)
            canSwitch = false;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == MainObjectContainer.Instance.Player2D)
            canSwitch = true;
    }

    public void SwitchTo3D(Cancellable cancellable)
    {
        if (canSwitch)
            cancellable.PerformCancellable(() => death = true, () => death = false);
        else
            cancellable.Cancel();
    }

}
