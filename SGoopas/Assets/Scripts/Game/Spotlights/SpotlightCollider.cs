using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightCollider : MonoBehaviour {
    [HideInInspector]
    public Spotlight.ShadowApplicatorType applicatorType;
    private ShadowApplicator applicator;
	// Use this for initialization
	void Start () {
        applicator = ShadowApplicatorFactory.CreateApplicatorFromType(applicatorType, this.gameObject);
	}

    void OnTriggerEnter(Collider other)
    {
        applicator.OnTriggerEnter(other);
    }

    void OnTriggerStay(Collider other)
    {
        applicator.OnTriggerStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        applicator.OnTriggerExit(other);
    }
}
