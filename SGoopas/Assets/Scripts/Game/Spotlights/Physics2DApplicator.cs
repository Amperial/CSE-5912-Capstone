using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Physics2DApplicator : ShadowApplicator
{
    private GameObject player;
    private Material shadowMaterial;
    private Material indicatorMaterial;

    private float offset = 0.1f;
    public Physics2DApplicator(GameObject spotLightCollider, Material shadowMaterial, Material indicatorMaterial)
    {
        this.shadowMaterial = shadowMaterial;
        this.indicatorMaterial = indicatorMaterial;
        player = MainObjectContainer.Instance.Player2D;
    }


    private static int AngleCompare(Vector2 x, Vector2 y){
        Vector2 top = new Vector2(1, 1);
        float anglex = Vector2.SignedAngle(top, x);
        float angley = Vector2.SignedAngle(top, y);

        if (anglex < 0)
            anglex = 360 + anglex;
        if (angley < 0)
            angley = 360 + angley;
        float angle = anglex - angley;

        if (angle > 0.00001f)
            return 1;
        else if (angle < -0.00001f)
            return -1;
        else
            return 0;
    }
    /*
     * Builds a 3D Mesh into mesh using the points found in poly
     */
    private void buildPolyMesh(PolygonCollider2D poly, Mesh mesh)
    {
        List<Vector2> points2d = new List<Vector2>(poly.points);
        points2d.Sort(AngleCompare);
        List<Vector3> points3d = new List<Vector3>();
        foreach(Vector2 point in points2d)
        {
            points3d.Add(new Vector3(point.x, point.y, offset));
        }
        List<int> triangles = new List<int>();
        for(int i = 0; i < points3d.Count - 2; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }

        mesh.vertices = points3d.ToArray();
        mesh.triangles = triangles.ToArray();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject != player)
        {
            if (collider.gameObject.tag != "Physics2D")
            {
                ChangeObject(collider);
                GenerateIndicator(collider.gameObject);
                collider.gameObject.tag = "Physics2D";
            }
        }
    }

    private void GenerateIndicator(GameObject original)
    {
        GameObject indicator = UnityEngine.Object.Instantiate(original, player.transform.root);
        UnityEngine.Object.Destroy(indicator.GetComponent<Rigidbody2D>());

        MeshRenderer renderer = indicator.GetComponent<MeshRenderer>();
        renderer.material = indicatorMaterial;

        indicator.GetComponent<Collider2D>().isTrigger = true;

        indicator.AddComponent<Physics2DIndicator>();
        indicator.tag = "Physics2D";
    }

    private void ChangeObject(Collider2D collider)
    {
        Rigidbody2D rb2d = collider.gameObject.GetComponent<Rigidbody2D>();
        if (!rb2d)
            rb2d = collider.gameObject.AddComponent<Rigidbody2D>();

        rb2d.bodyType = RigidbodyType2D.Dynamic;

        MeshFilter meshFilter = collider.gameObject.GetComponent<MeshFilter>();
        if (!meshFilter)
            meshFilter = collider.gameObject.AddComponent<MeshFilter>();

        MeshRenderer renderer = collider.gameObject.GetComponent<MeshRenderer>();
        if (!renderer)
            renderer = collider.gameObject.AddComponent<MeshRenderer>();

        renderer.material = shadowMaterial;


        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        PolygonCollider2D poly = collider as PolygonCollider2D;
        if (poly)
        {
            buildPolyMesh(poly, mesh);
        }
        else
        {
            //maybe add some logic for other colliders here, if we ever have them
        }
    }


    public void OnTriggerExit2D(Collider2D collider)
    {

    }

    public void OnTriggerStay2D(Collider2D collider)
    {

    }
}

