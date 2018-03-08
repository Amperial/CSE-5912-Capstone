using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator3D{

    Animator anim;
    public Animator3D(GameObject player3D)
    {
        anim = player3D.GetComponent<Animator>();
    }

    public void StopRun()
    {
        anim.SetBool("run", false);
    }
    public void StartRun()
    {
        anim.SetBool("run", true);
    }
    public void Jump()
    {
        anim.SetTrigger("jump");
    }
    public void JumpInPlace()
    {
        anim.SetTrigger("JIP");
    }
    public void StartGrab()
    {
        anim.SetBool("grab", true);
    }
    public void ReleaseGrab()
    {
        anim.SetBool("grab", false);
    }
    public void StartPush()
    {
        anim.SetBool("grabAct", true);
    }
    public void StopPush()
    {
        anim.SetBool("grabAct", false);
    }
}
