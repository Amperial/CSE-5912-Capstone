using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded {

    public static bool IsGrounded(Vector3 playerPosition, float characterWidth, Vector3 ground){
        Vector3 tempVL = playerPosition;
        Vector3 tempVR = playerPosition;
        tempVL.x = playerPosition.x - (characterWidth / 2.0f);
        tempVR.x = playerPosition.x + (characterWidth / 2.0f);

        if (Physics2D.Linecast(playerPosition, ground, ~(1 << LayerMask.NameToLayer("Player"))) || Physics2D.Linecast(tempVL, ground, ~(1 << LayerMask.NameToLayer("Player"))) || Physics2D.Linecast(tempVR, ground, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
