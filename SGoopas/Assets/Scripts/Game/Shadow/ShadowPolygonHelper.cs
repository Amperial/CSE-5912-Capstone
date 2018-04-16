using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShadowPolygonHelper
{
    /*
	 * Calculates and places in the scene a circle collider on the gameObject, scaled to represent the ellipse formed by the intersection of the spotlight light and the plane wallPlane
	 */
    public static void MakeSpotlightCollider(Light light, Plane wallPlane, GameObject gameObject)
    {
        Vector3 lightDir = light.transform.forward.normalized;
        float spotAngle = light.spotAngle/2;
        Vector3 wallNormal = wallPlane.normal;
        if (lightDir == -wallNormal)
        {
            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.radius = 1.0f;
            Vector3 centerPoint = GetRayPlaneIntersection(light.transform.position, lightDir, wallPlane.normal, wallPlane.distance);
            float height = (light.transform.position - centerPoint).magnitude;
            float scale = height * Mathf.Tan(Mathf.Deg2Rad * spotAngle);
            gameObject.transform.position = centerPoint;
            gameObject.transform.localScale = new Vector3(scale, scale, 1);
            Debug.Log("Circle Option");
        }
        else
        {
            PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
            collider.isTrigger = true;

            MakeJankCircle(collider, 100);

            Vector3 minorAxis = Vector3.Cross(wallNormal, lightDir);
            Vector3 majorAxis = Vector3.Cross(wallNormal, minorAxis);
            Vector3 firstMajorVector, secondMajorVector, firstMinorVector, secondMinorVector;
            firstMajorVector = Quaternion.AngleAxis(spotAngle, minorAxis) * lightDir;
            secondMajorVector = Quaternion.AngleAxis(-spotAngle, minorAxis) * lightDir;
            firstMinorVector = Quaternion.AngleAxis(spotAngle, majorAxis) * lightDir;
            secondMinorVector = Quaternion.AngleAxis(-spotAngle, majorAxis) * lightDir;
            firstMajorVector.Normalize();
            secondMajorVector.Normalize();
            firstMinorVector.Normalize();
            secondMinorVector.Normalize();

            Vector3 firstMajorPoint, secondMajorPoint, firstMinorPoint;
            firstMajorPoint = GetRayPlaneIntersection(light.transform.position, firstMajorVector, wallPlane.normal, wallPlane.distance);
            secondMajorPoint = GetRayPlaneIntersection(light.transform.position, secondMajorVector, wallPlane.normal, wallPlane.distance);
            firstMinorPoint = GetRayPlaneIntersection(light.transform.position, firstMinorVector, wallPlane.normal, wallPlane.distance);

            Vector3 centerPoint = (firstMajorPoint + secondMajorPoint) / 2;

            float rotateAngle = Vector3.SignedAngle(Vector3.up, minorAxis, Vector3.forward);
            gameObject.transform.Rotate(Vector3.forward, rotateAngle);
            gameObject.transform.position = centerPoint;

            float xScale = (firstMajorPoint - centerPoint).magnitude;
            float yScale = (firstMinorPoint - centerPoint).magnitude;

            gameObject.transform.localScale = new Vector3(xScale, yScale, 1);
            Debug.Log("Ellipse option");
        }
    }

    //Unity won't allow circle colliders to be skewed, so here's a unit "circle" collider implemented using a PolygonCollider2D that CAN be skewed
    private static void MakeJankCircle(PolygonCollider2D collider, int numPoints)
    {
        List<Vector2> points = new List<Vector2>();

        float angle = 0;

        for (int i = 0; i <= numPoints; i++)
        {

            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            points.Add(new Vector2(x, y));
            angle += 360f / numPoints;
        }

        collider.points = points.ToArray();
    }

    /*
	 * Calculates raycast shadow points on a plane for point vs. directional lighting. 
	 */
    private static List<Vector3> GetShadowPoints(Light light, GameObject gameObject, Plane wallPlane) {
        List<Vector3> points;
        switch (light.type)
        {
            case LightType.Directional:
                points = GetDirectionalLightShadow(light.transform.forward, gameObject, wallPlane);
                break;

            case LightType.Point:
            default:
                points = GetPointLightShadow(light.transform.position, gameObject, wallPlane);
                break;
        }
        return points;
    }

    /*
		Returns the points on wallPlane that represent the shadow casted by the light on the gameObject onto the wallPlane
	*/
    public static List<Vector3> GetPointLightShadow(Vector3 lightPos, GameObject gameObject, Plane wallPlane)
    {
        List<Vector3> rays = new List<Vector3>();
        List<Vector3> wallIntersections = new List<Vector3>();

        //Get mesh vertices
        List<Vector3> meshVertices = GetWorldVertices(gameObject);
        List<Vector3> verticesFront = new List<Vector3>();
        List<Vector3> verticesBehind = new List<Vector3>();

        foreach(Vector3 v in meshVertices){
            if(v.z < lightPos.z){
                verticesBehind.Add(v);
            }else{
                verticesFront.Add(v);
            }
        }

        //Cancel if object is entirely behind light
        if(verticesFront.Count == 0){
            return null;
        }

        //If there exists polygon points behind the light
        if(verticesBehind.Count > 0){
            //tempPlane placed slightly in front of the light
            Plane tempPlane = new Plane(lightPos + new Vector3(0,0, .001f), 
                                        lightPos + new Vector3(1,0, .001f), 
                                        lightPos + new Vector3(0,1, .001f));

            List<Vector3> tempPlaneIntersections = new List<Vector3>();

            //Cast rays from front verteces to back verteces
            foreach(Vector3 f in verticesFront){
                foreach(Vector3 b in verticesBehind){
                    //Get intersection of rays and tempPlane
                    Vector3 intersection = GetRayPlaneIntersection(f, b-f, tempPlane.normal, tempPlane.distance);
                    tempPlaneIntersections.Add(intersection);
                }
            }

            verticesFront.AddRange(tempPlaneIntersections);         
        }

        //Determine direction of object vertices
        foreach (Vector3 v in verticesFront)
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
		Returns the points on wallPlane that represent the shadow casted by a directional light on the gameObject onto the wallPlane
	*/
    private static List<Vector3> GetDirectionalLightShadow(Vector3 lightDir, GameObject gameObject, Plane wallPlane)
    {
        List<Vector3> wallIntersections = new List<Vector3>();

        //Get mesh vertices
        List<Vector3> meshVertices = GetWorldVertices(gameObject);

        //Get intersection of lightDir and plane
        foreach (Vector3 v in meshVertices)
        {
            Vector3 intersection = GetRayPlaneIntersection(v, lightDir, wallPlane.normal, wallPlane.distance);
            wallIntersections.Add(intersection);
        }

        return wallIntersections;
    }

    /*
		Returns the vertices of a mesh in world space coordinates.
	*/
    private static List<Vector3> GetWorldVertices(GameObject gameObject)
    {
        Mesh m = gameObject.GetComponent<MeshFilter>().mesh;

        int[] triangles = m.triangles;
        Vector3[] vertices = m.vertices;

        List<Vector3> transformedVertices = new List<Vector3>();

        //Convert mesh's localspace points to worldspace points
        for (int i = 0; i < vertices.Length; i++)
        {
            transformedVertices.Add(gameObject.transform.TransformPoint(vertices[i]));
        }

        return transformedVertices;
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

	public static void CalculateShadowForGameObject(GameObject shadowWithCollider, GameObject castingObject, Light light, Plane wallPlane)
    {
        List<Vector3> points = GetShadowPoints(light, castingObject, wallPlane);
        if(points == null){
            return;
        }
		CalculateShadowFromCastPoints(points, wallPlane, shadowWithCollider);
		shadowWithCollider.name = castingObject.name + " Shadow";
    }

	private static void CalculateShadowFromCastPoints (List<Vector3> points, Plane wallPlane, GameObject shadowWithCollider)
    {
		List<Vector2> points2D = ChangeOfBase3Dto2D(points, wallPlane, shadowWithCollider);
		ConvexHullPolygon2D(points2D, shadowWithCollider);
    }

    public static GameObject CreateShadowGameObject (List<Vector3> points, Plane wallPlane)
    {
        //Cancel if points are null
        if(points == null){
            return null;
        }

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