using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DControl : MonoBehaviour
{

    [HideInInspector] public bool jump = false;
    //private bool grounded = false;

    private Rigidbody2D rb;
    public float moveForce = 20f;
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