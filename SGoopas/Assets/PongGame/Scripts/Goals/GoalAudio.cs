using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAudio : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Ball"))
        {
            AudioSource scoreSound = this.GetComponent<AudioSource>();
            scoreSound.Play();
        }
    }
}
