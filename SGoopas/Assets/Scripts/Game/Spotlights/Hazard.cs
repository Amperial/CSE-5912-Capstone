using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [HideInInspector]
    public GameObject spotlightCollider;

    private GameObject player2D;

    private bool hasHurtPlayer;
    private ShadowApplicator applicator;
    private Collider2D spotlightC;

    // Use this for initialization
    void Start()
    {
        player2D = MainObjectContainer.Instance.Player2D;
        spotlightC = spotlightCollider.GetComponent<Collider2D>();
        hasHurtPlayer = false;
    }

    private bool shouldPlayerHurt(Collision2D collision)
    {
        if (collision.gameObject == player2D)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (spotlightC.OverlapPoint(point.point))
                {
                    hasHurtPlayer = true;
                    return true;
                }
            }
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shouldPlayerHurt(collision))
            PlayerDeathHandler.TriggerPlayerDeath();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!hasHurtPlayer && shouldPlayerHurt(collision))
            PlayerDeathHandler.TriggerPlayerDeath();       
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player2D)
        {
            hasHurtPlayer = false;
        }
    }

}
