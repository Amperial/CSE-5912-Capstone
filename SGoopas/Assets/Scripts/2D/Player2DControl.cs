using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static ShadowPolygonHelper;

public class Player2DControl : MonoBehaviour
{

    [HideInInspector] public bool jump = false;
    //private bool grounded = false;

    private Rigidbody2D rb;
    public float moveForce = 20f;
    private GameObject testCube;
    private GameObject light;

    private float maxSpeed = 5f;
    private float jumpForce = 1000f;
    public Transform groundCheck;
    List<Vector3> shadowPoints;
	PolygonCollider2D shadowCollider;
	GameObject shadow;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        testCube = GameObject.Find("TestCube");
        light = GameObject.Find("light");

        Plane wallPlane = new Plane(new Vector3(0, 0, -1).normalized, 0);
        shadowPoints = ShadowPolygonHelper.GetPointLightShadow(light.transform.position, testCube, wallPlane);
        shadow = ShadowPolygonHelper.CreateShadowGameObject(shadowPoints, wallPlane);
		shadowCollider = shadow.GetComponent<PolygonCollider2D>();
		Vector2[] newPoints = (Vector2[])shadowCollider.points.Clone();
		for(int i = 0; i < shadowCollider.points.Length; i++){
			newPoints[i].x = shadowCollider.points[i].x + shadow.transform.position.x;
			newPoints[i].y = shadowCollider.points[i].y + shadow.transform.position.y;

		}
		shadowCollider.points = newPoints;
        Rigidbody2D shadowRB = shadow.AddComponent<Rigidbody2D> ();
        shadowRB.bodyType = RigidbodyType2D.Dynamic;
        shadowRB.gravityScale = 0;
    }



    void Update()
    {
    
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		foreach(Vector2 v in shadowCollider.points){
			Debug.DrawLine(light.transform.position, new Vector3(v.x, v.y, 0));
        }

//		foreach(Vector2 v in shadowPoints){
//			Debug.DrawLine(light.transform.position, v);
//		}

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            Debug.Log("JUMP");
        }
    }
    // FixedUpdate for physics update
    void FixedUpdate()
    {
        //store the current horizontal input in the float moveHorizontal
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        //Set horizontal movement
            rb.AddForce(Vector2.right * moveHorizontal * moveForce);
        //Cap horizontal movement
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

        if (jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

}