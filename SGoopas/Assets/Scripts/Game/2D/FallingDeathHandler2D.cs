using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDeathHandler2D : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.Equals(MainObjectContainer.Instance.Player2D))
        {
            PlayerDeathHandler.TriggerPlayerDeath();
        }
    }
}
