﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShadowPolygonHelper
{
    /*
		Rerturns the points on wallPlane that represent the shadow casted by the light on the gameObject onto the wallPlane
	*/
    public static List<Vector3> GetPointLightShadow(Vector3 lightPos, GameObject gameObject, Plane wallPlane)
    {
        List<Vector3> rays = new List<Vector3>();
        List<Vector3> wallIntersections = new List<Vector3>();

        //Get edge vertices
        List<Vector3> edgeVerts = GetEdgeVertices(lightPos, gameObject);

        //Determine direction of object vertices
        foreach (Vector3 v in edgeVerts)
        {
            rays.Add((lightPos - v).normalized);
        }

        //Get intersection of ray and plane
        foreach (Vector3 ray in rays)
        {
            Vector3 intersection = GetRayPlaneIntersection(lightPos, ray, wallPlane.normal, wallPlane.distance);
            wallIntersections.Add(intersection);
            Debug.DrawLine(lightPos, intersection);
        }

        return wallIntersections;
    }

    /*
		Returns edge vertices of a mesh that are visible from perspective.
	*/
    private static List<Vector3> GetEdgeVertices(Vector3 perspective, GameObject gameObject)
    {
        Mesh m = gameObject.GetComponent<MeshFilter>().mesh;
        List<Vector3> visibleVerts = new List<Vector3>();
        List<Vector3> invisibleVerts = new List<Vector3>();

        int[] triangles = m.triangles;
        Vector3[] vertices = m.vertices;

        //Convert mesh's localspace points to worldspace points
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = gameObject.transform.TransformPoint(vertices[i]);
        }

        //Iterate over each triangular face of the mesh
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Plane objectFace = new Plane(
                          vertices[triangles[i]],
                          vertices[triangles[i + 1]],
                          vertices[triangles[i + 2]]);

            //If plane is visible to the light, add each point to the list of visible vertices
            if (IsPlaneFacingPoint(objectFace, perspective))
            {
                visibleVerts.Add(vertices[triangles[i]]);
                visibleVerts.Add(vertices[triangles[i + 1]]);
                visibleVerts.Add(vertices[triangles[i + 2]]);
            }
            else
            { //Otherwise...
                invisibleVerts.Add(vertices[triangles[i]]);
                invisibleVerts.Add(vertices[triangles[i + 1]]);
                invisibleVerts.Add(vertices[triangles[i + 2]]);
            }
        }

        //Edge vertices are the intersection of ligh-facing plane vertices and nonlight-facing plane vertices
        return invisibleVerts.Intersect(visibleVerts).Distinct().ToList();
    }

    /*
		Determines if plane is facing a given point
	*/
    private static bool IsPlaneFacingPoint(Plane p, Vector3 point)
    {
        return Vector3.Dot(p.normal, point - p.normal) > 0;
    }

    /*
		Get ray-plane intersection point given ray starting point, ray direction, 
		plane's normal, and plane's distance from origin.
	*/
    private static Vector3 GetRayPlaneIntersection(Vector3 rayStart, Vector3 rayDir, Vector3 planeNormal, float planeDistance)
    {
        float x = Vector3.Dot(rayStart, planeNormal) + planeDistance;
        float t = -(x) / (Vector3.Dot(rayDir, planeNormal));
        return rayStart + (t * rayDir);
    }

    public static GameObject CreateShadowGameObject(GameObject gameObject, Vector3 lightPosition, Plane wallPlane)
    {
        return CreateShadowGameObject(GetPointLightShadow(lightPosition, gameObject, wallPlane), wallPlane);
    }

    public static GameObject CreateShadowGameObject (List<Vector3> points, Plane wallPlane)
    {
        GameObject shadow = new GameObject();
        List<Vector2> points2D = ChangeOfBase3Dto2D(points, wallPlane, shadow);

        shadow.AddComponent<PolygonCollider2D>();
        ConvexHullPolygon2D(points2D, shadow);

        return shadow;
    }
    /*
        Returns a Polygon2D representing the points in the array
    */
    private static void ConvexHullPolygon2D(List<Vector2> point, GameObject polygonObject)
    {
        PolygonCollider2D polygonCollider = polygonObject.GetComponent<PolygonCollider2D>();
        List<Vector2> points = new List<Vector2>(point);

        //Sort the points first by x value, then by y value
        points.Sort((p1, p2) =>
            p1.x == p2.x ? p1.y.CompareTo(p2.y) : (p1.x > p2.x ? 1 : -1));

        //Using a LinkedList as it allows for constant time insertions at the beginning and end.
        LinkedList<Vector2> hull = new LinkedList<Vector2>();
        //keep track of lower (end of list) and upper (beginning of list) convex hulls
        int lower = 0, upper = 0;

        for(int i = points.Count - 1; i>=0; i--)
        {
            Vector2 p = points[i];
            Vector2 comparePoint;

            //modify the lower hull
            while(lower >= 2 && MathHelpers.CrossProduct(((comparePoint = hull.Last.Value) - hull.Last.Previous.Value), (p - comparePoint)) >= 0)
            {
                hull.RemoveLast();
                lower--;
            }
            hull.AddLast(p);
            lower++;

            //modify the upper hull
            while (upper >= 2 && MathHelpers.CrossProduct(((comparePoint = hull.First.Value) - hull.First.Next.Value), (p - comparePoint)) <= 0)
            {
                hull.RemoveFirst();
                upper--;
            }
            //Don't want to add the same point twice, so don't add to upper hull if upper == 0
            if (upper != 0)
                hull.AddFirst(p);
            upper++;
        }
        hull.RemoveLast();

        //Now take the hull, create an array from it, and use it to modify the PolygonCollider2D component
        Vector2[] pointsArray = new Vector2[hull.Count];
        hull.CopyTo(pointsArray, 0);

        polygonCollider.points = pointsArray;
    }

    
    /*
        Returns the 2D representation of the 3D points on the wallPlane. Requires that all the 3D points lie on the wallPlane
    */
    private static List<Vector2> ChangeOfBase3Dto2D(List<Vector3> points3D, Plane wallPlane, GameObject polygonObject)
    {
        List<Vector2> points2D = new List<Vector2>();
        //Calculate the change of basis
        Vector3 normal = wallPlane.normal;
        //Picks some arbitrary vectors that span the plane
        Vector3 axis1;
        Vector3 axis2;
        if (normal.x != 0) {
            axis1 = new Vector3(normal.y / normal.x, -1, 0);
            axis2 = new Vector3(normal.z / normal.x, 0, -1);
        } else if (normal.y != 0)
        {
            axis1 = new Vector3(-1, normal.x / normal.y, 0);
            axis2 = new Vector3(0, normal.z / normal.y, -1);
        }
        else //the normal can't be [0,0,0], so Z can't be 0 if the two earlier tests failed
        {
            axis1 = new Vector3(-1, 0, normal.x / normal.z);
            axis2 = new Vector3(0, -1, normal.y / normal.z);
        }
        //Transform our ugly basis into an orthanormal one
        axis1 = axis1.normalized;
        axis2 = axis2 - Vector3.Dot(axis1, axis2) * axis1;
        axis2 = axis2.normalized;

        //Construct the matrix representing the basis
        Matrix4x4 basis = new Matrix4x4(new Vector4(axis1.x, axis1.y, axis1.z, 0), new Vector4(axis2.x, axis2.y, axis2.z, 0), new Vector4(normal.x, normal.y, normal.z, 0), new Vector4(0, 0, 0, 1));


        //invert the basis

        Matrix4x4 invertedBasis = basis.inverse;

        //Use the inverted basis to find the 2d points
        Vector2 average2D = new Vector2(0, 0);
        Vector3 average3D = new Vector3(0, 0, 0);
        List<Vector2> points2DTemp = new List<Vector2>();

        foreach (Vector3 p3 in points3D)
        {
            average3D = average3D + p3;
            Vector4 p4 = new Vector4(p3.x, p3.y, p3.z, 1);
            Vector4 p4Transformed = invertedBasis * p4;
            //After the transformation we can throw out the w value, which will always be 1, and the z value, which represents the offset from the plane
            Vector2 p2Temp = new Vector2(p4Transformed.x, p4Transformed.y);
            average2D = average2D + p2Temp;
            points2DTemp.Add(p2Temp);
        }
        average2D = average2D / points3D.Count;
        average3D = average3D / points3D.Count;
        
        foreach (Vector2 p2Temp in points2DTemp)
        {
            Vector2 p2 = p2Temp - average2D;
            points2D.Add(p2);
        }

        //Set the transform of the polygonObject to place the shadow in 3d space
        polygonObject.transform.rotation = MathHelpers.QuaternionFromMatrix(basis);
        polygonObject.transform.position = average3D;

        return points2D;
    }

    
}