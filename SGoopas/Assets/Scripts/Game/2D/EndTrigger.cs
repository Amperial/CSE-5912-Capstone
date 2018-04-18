using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private Animator anim;
    //private GameObject player;
    private bool exit;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //player = MainObjectContainer.Instance.Player2D;
        exit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ExitHandler.TriggerExit();
        anim.SetBool("exit", true);
        StartCoroutine(ExitLevel());
    }

    //TODO Create a 3 or 4 second delay before ending the level to allow the animation to play.
    
    IEnumerator ExitLevel()
    {
        //This solution is awful and won't work in an asynchronous environment. Thankfully, coroutines aren't actually asynchronous
        if (!exit)
        {
            exit = true;
            yield return new WaitForSeconds(1.8f);
            MasterStateMachine.Instance.GoToNextLevel();
        }

    }
    
}
