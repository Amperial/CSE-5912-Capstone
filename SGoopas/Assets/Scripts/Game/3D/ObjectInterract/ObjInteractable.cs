using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInteractable : MonoBehaviour {

    public enum ObjectType { pushPull, lift};
    public ObjectType objType;
    public Shader normal;
    public void Awake()
    {
        normal = gameObject.GetComponent<Renderer>().material.shader;
    }
}
