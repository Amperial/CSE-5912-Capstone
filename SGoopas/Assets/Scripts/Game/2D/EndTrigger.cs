using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        MasterStateMachine.Instance.setState(new CreditsState());
    }
}
