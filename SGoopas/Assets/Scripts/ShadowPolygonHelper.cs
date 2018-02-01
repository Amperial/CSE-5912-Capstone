using System.Collections.Generic;
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

    public PolygonCollider2D GetShadowCollider(List<Vector3> points, Plane wallPlane)
    {
        List<Vector2> points2D = ChangeOfBase3Dto2D(points, wallPlane);
        return GetConvexHullPolygon2D(points2D);
    }
    /*
        Returns a Polygon2D representing the points in the array
    */
    public static PolygonCollider2D GetConvexHullPolygon2D(List<Vector2> points)
    {
        PolygonCollider2D polygonCollider = new PolygonCollider2D();
        //TODO: Actually find the convex hull
        return polygonCollider;
    }
    /*
        Returns the 2D representation of the 3D points on the wallPlane. Requires that all the 3D points lie on the wallPlane
    */
    public static List<Vector2> ChangeOfBase3Dto2D(List<Vector3> points3D, Plane wallPlane)
    {
        List<Vector2> points2D = new List<Vector2>();
        //TODO: Actually change base
        return points2D;
    }
}