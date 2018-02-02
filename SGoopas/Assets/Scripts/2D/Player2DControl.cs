using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DControl : MonoBehaviour
{

    [HideInInspector] public bool jump = false;
    //private bool grounded = false;

    private Rigidbody2D rb;
    public float moveForce = 365f;
    private float maxSpeed = 5f;
    private float jumpForce = 1000f;
    public Transform groundCheck;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            Debug.Log("JUMP");
        }
    }
    // FixedUpdate for physics update
    void FixedUpdate()
    {
        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            rb.AddRelativeForce(new Vector3(0f, speed * 100f, 0f));

        if (Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector3(speed* 20f, 0f, 0f));
        else if (Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector3(speed * -20f, 0f, 0f));

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity.x = rb.velocity.normalized * maxSpeed;
        }*/

        //store the current horizontal input in the float moveHorizontal
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Store the current vertical input in the float moveVertical.
        //float moveVertical = Input.GetAxis("Vertical");
        
        //Set horizontal movement
        if (moveHorizontal * rb.velocity.x < maxSpeed)
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