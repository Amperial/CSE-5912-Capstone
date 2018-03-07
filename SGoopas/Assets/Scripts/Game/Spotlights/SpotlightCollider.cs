using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightCollider : MonoBehaviour {
    [HideInInspector]
    public Spotlight.ShadowApplicatorType applicatorType;
    [HideInInspector]
    public GameObject player2D;

    private ShadowApplicator applicator;

	// Use this for initialization
	void Start () {
        applicator = ShadowApplicatorFactory.CreateApplicatorFromType(this.applicatorType, this.gameObject, this.player2D);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        applicator.OnTriggerEnter2D(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        applicator.OnTriggerStay2D(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        applicator.OnTriggerExit2D(other);
    }
}
