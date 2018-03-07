using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator2D : MonoBehaviour {

    //Updates the speedX and speedY parameters for the Animator state machine
    public static void updateXYParam(Animator anim, Rigidbody2D rb)
    {
        anim.SetFloat("speedX", System.Math.Abs(rb.velocity.x));
        anim.SetFloat("speedY", rb.velocity.y);
    }

    //Updates the grounded parameter for the Animator state machine
    public static void updateGroundedParam(Animator anim, bool grounded)
    {
        anim.SetBool("grounded", grounded);
    }
}
