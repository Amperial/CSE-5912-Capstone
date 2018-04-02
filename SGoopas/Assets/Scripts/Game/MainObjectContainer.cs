using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MainObjectContainer : MonoBehaviour {
    public GameObject Player2D;
    public GameObject Player3D;
    public GameObject ShadowPlane;
    private static MainObjectContainer instance;

    public static MainObjectContainer Instance {
        get {
            Assert.IsNotNull(instance, "Your level scene is not setup properly. Add a MainObjectContainer to the root node of your scene.");
            return instance;
        }
    }

    public static void Reset()
    {
        instance = null;
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        string nullObjectError = "Your MainObjectContainer scene is not setup properly. Make sure Player2D, Player3D, and ShadowPlane are connected in the editor.";
        Assert.IsNotNull(Player2D, nullObjectError);
        Assert.IsNotNull(Player3D, nullObjectError);
        Assert.IsNotNull(ShadowPlane, nullObjectError);
    }
}
