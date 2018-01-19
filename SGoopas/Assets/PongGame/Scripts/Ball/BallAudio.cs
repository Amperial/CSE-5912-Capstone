using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        AudioSource boingSound = this.GetComponent<AudioSource>();
        boingSound.Play();
    }
}
