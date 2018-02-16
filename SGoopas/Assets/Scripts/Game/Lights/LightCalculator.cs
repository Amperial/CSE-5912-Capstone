using UnityEngine;

public class LightCalculator
{
    public GameObject ShadowObjects;
    public Light light;
    public GameObject plane;
    
    public LightCalculator(Light light, GameObject plane){
        this.light = light;
        this.plane = plane;
    }

    public Vector3 Direction
    {
        get { return light.transform.forward; }
    }

    public Vector3 Position
    {
        get { return light.transform.position; }
    }

    public Plane Plane
    {
        get { return new Plane(plane.transform.up.normalized, new Vector3()); }
    }
}