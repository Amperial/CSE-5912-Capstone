using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HazardApplicator : ShadowApplicator
{
    private GameObject spotLightCollider;
    private GameObject player;
    public HazardApplicator(GameObject spotLightCollider, GameObject player)
    {
        this.spotLightCollider = spotLightCollider;
        this.player = player;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject != player)
        {
            Hazard hazard = collider.gameObject.AddComponent<Hazard>();
            hazard.spotlightCollider = spotLightCollider;
            hazard.player2D = player;
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

