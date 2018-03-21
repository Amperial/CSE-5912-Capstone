using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInteractable : MonoBehaviour {

    public enum ObjectType { pushPull, lift};
    public ObjectType objType;
    public Shader original;
    public void Awake()
    {
        original = gameObject.GetComponent<Renderer>().material.shader;
    }

}
