using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
       anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //StartCoroutine(ExitLevel());
        anim.SetBool("exit", true);
        MasterStateMachine.Instance.GoToNextLevel();
    }

    //TODO Create a 3 or 4 second delay before ending the level to allow the animation to play.
    /*
    IEnumerator ExitLevel()
    {
        print("Hey it's the end!");
        yield return new WaitForSeconds(0);
    }
    */
}
