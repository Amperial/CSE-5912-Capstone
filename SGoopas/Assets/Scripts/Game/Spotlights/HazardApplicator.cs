using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HazardApplicator : ShadowApplicator
{
    private GameObject spotLightCollider;
    private GameObject player;
    public HazardApplicator(GameObject spotLightCollider)
    {
        this.spotLightCollider = spotLightCollider;
        player = MainObjectContainer.Instance.Player2D;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject != player)
        {
            Hazard hazard = collider.gameObject.AddComponent<Hazard>();
            hazard.spotlightCollider = spotLightCollider;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject != player)
        {
            Hazard hazard = collider.gameObject.GetComponent<Hazard>();
            if (hazard != null)
                UnityEngine.Object.Destroy(hazard);
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {

    }
}

