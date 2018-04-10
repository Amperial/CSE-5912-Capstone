using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightCollider : MonoBehaviour {
    [HideInInspector]
    public Spotlight.ShadowApplicatorType applicatorType;
    [HideInInspector]
    public Material ShadowMaterial;
    [HideInInspector]
    public Material IndicatorMaterial;

    private ShadowApplicator applicator;

	// Use this for initialization
	void Start () {
        applicator = ShadowApplicatorFactory.CreateApplicatorFromType(this.applicatorType, this.gameObject, this.ShadowMaterial, this.IndicatorMaterial);
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
