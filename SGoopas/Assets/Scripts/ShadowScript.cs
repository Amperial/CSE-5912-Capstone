using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ShadowScript : MonoBehaviour {
	public GameObject cube;
	private Mesh mesh;
	public GameObject planeObject;
	public Plane plane;
	public LineRenderer lineRenderer;
	public Text fps;
	List<Vector3> edgeVerts;
	List<Vector3> rays = new List<Vector3> ();
	List<Vector3> wallIntersections = new List<Vector3> ();

	void Start () {
		mesh = cube.GetComponent<MeshFilter>().mesh;
		plane = new Plane (new Vector3 (0, 0, -1).normalized, 10);
	}
	
	// Update is called once per frame
	void Update () {
		//Output/input
		fps.text = "FPS: " + 1.0 / Time.deltaTime;
		CheckInputs (Time.deltaTime);

		//reset rays and intersections
		rays.Clear();
		wallIntersections.Clear ();

		//Get edge vertices
		edgeVerts = GetEdgeVertices (mesh);

		//Determine direction of object vertices
		foreach (Vector3 v in edgeVerts) {
			rays.Add ((gameObject.transform.position - v).normalized);
		}

		//Get intersection of ray and plane
		foreach(Vector3 ray in rays) {
			Vector3 intersection = GetRayPlaneIntersection (gameObject.transform.position, ray, plane.normal, plane.distance);
			wallIntersections.Add (intersection);
		}

		//Draw rays
		foreach(Vector3 intersection in wallIntersections) {
			Debug.DrawLine(gameObject.transform.position, intersection);
		}
	}

	/*
		Get ray-plane intersection point given ray starting point, ray direction, 
		plane's normal, and plane's distance from origin.
	*/
	private Vector3 GetRayPlaneIntersection(Vector3 rayStart, Vector3 rayDir, Vector3 planeNormal, float planeDistance){
		float x = Vector3.Dot (rayStart, planeNormal) + planeDistance;
		float t = - (x)/(Vector3.Dot(rayDir, planeNormal));
		return rayStart + (t * rayDir);
	}

	/*
		Returns edge vertices of a mesh that are visible to gameObject.
	*/
	private List<Vector3> GetEdgeVertices(Mesh m){
		List<Vector3> visibleVerts = new List<Vector3> ();
		List<Vector3> invisibleVerts = new List<Vector3> ();

		int[] triangles = m.triangles;
		Vector3[] vertices = m.vertices;

		//Convert mesh's localspace points to worldspace points
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = cube.transform.TransformPoint (vertices [i]);
		}

		//Iterate over each triangular plane of the mesh
		for (int i = 0; i < triangles.Length; i += 3) {
			Plane p = new Plane (
				          vertices [triangles [i]],
				          vertices [triangles [i + 1]],
				          vertices [triangles [i + 2]]);

			//If plane is visible to the light, add each point to the list of visible vertices
			if (IsPlaneFacingPoint (p, gameObject.transform.position)) {
				visibleVerts.Add (vertices [triangles [i]]);
				visibleVerts.Add (vertices [triangles [i + 1]]);
				visibleVerts.Add (vertices [triangles [i + 2]]);
			} else { //Otherwise...
				invisibleVerts.Add (vertices [triangles [i]]);
				invisibleVerts.Add (vertices [triangles [i + 1]]);
				invisibleVerts.Add (vertices [triangles [i + 2]]);
			}
		}

		//Edge vertices are the intersection of ligh-facing plane vertices and nonlight-facing plane vertices
		return invisibleVerts.Intersect (visibleVerts).Distinct().ToList();
	}

	/*
		Determines if plane is facing a given point
	*/
	private bool IsPlaneFacingPoint(Plane p, Vector3 point){
		return Vector3.Dot (p.normal, point - p.normal) > 0;
	}

	/*
		Inputs for object transform
	*/
	private void CheckInputs(float dt){
		//X Rotation
		if (Input.GetKey (KeyCode.Q)) {
			cube.transform.Rotate (new Vector3 (50, 0, 0) * dt);
		} else if (Input.GetKey (KeyCode.A)) {
			cube.transform.Rotate (new Vector3 (-50, 0, 0) * dt);
		}

		//Y Rotation
		if (Input.GetKey(KeyCode.W)) {
			cube.transform.Rotate (new Vector3 (0, 50, 0) * dt);
		} else if (Input.GetKey (KeyCode.S)) {
			cube.transform.Rotate (new Vector3 (0, -50, 0) * dt);
		}

		//Z Rotation
		if (Input.GetKey(KeyCode.E)) {
			cube.transform.Rotate (new Vector3 (0, 0, 50) * dt);
		} else if (Input.GetKey(KeyCode.D)) {
			cube.transform.Rotate (new Vector3 (0, 0, -50) * dt);
		}

		//X Translation
		if (Input.GetKey (KeyCode.RightArrow)) {
			cube.transform.position += new Vector3 (1, 0, 0)*dt;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			cube.transform.position -= new Vector3 (1, 0, 0)*dt;
		}

		//Z Translation
		if (Input.GetKey(KeyCode.UpArrow)) {
			cube.transform.position += new Vector3 (0, 0, 3)*dt;
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			cube.transform.position -= new Vector3 (0, 0, 3)*dt;
		}
	}
}