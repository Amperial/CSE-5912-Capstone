using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckDebug2D : MonoBehaviour {
    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Collider2D parentCollider = gameObject.transform.parent.gameObject.GetComponent<Collider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(parentCollider.bounds.size.x, 0.1f, 0.1f));
    }
    #endif
}
