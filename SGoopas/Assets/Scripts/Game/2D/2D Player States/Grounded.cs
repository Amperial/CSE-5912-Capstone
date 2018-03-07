using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour {

    public static bool IsGrounded(Vector3 playerPosition, float characterWidth, Vector3 ground, int layers){
        Vector3 tempVL = playerPosition;
        Vector3 tempVR = playerPosition;
        tempVL.x = playerPosition.x - (characterWidth / 2.0f);
        tempVR.x = playerPosition.x + (characterWidth / 2.0f);

        if (Physics2D.Linecast(playerPosition, ground, layers) || Physics2D.Linecast(tempVL, ground, layers)|| Physics2D.Linecast(tempVR, ground, layers))
        {
            return true;
            Debug.Log("raycast hit");
        }
        else
        {
            return false;
        }
    }
}
