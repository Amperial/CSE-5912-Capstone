using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScore : MonoBehaviour {
    public Score score;
    void OnTriggerEnter(Collider other)
    {
        print("On Trigger Enter Goal Score: " + other.gameObject.name);
        if (other.gameObject.name.Equals("Ball"))
        {
            score.IncrementScore();
            transform.root.gameObject.BroadcastMessage("RestartGame", false); ;
        }
    }
}
