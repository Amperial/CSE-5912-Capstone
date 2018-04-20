using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler { 
    public delegate void EnemyCollisionOccured(GameObject enemy);
    public static event EnemyCollisionOccured EnemyCollisionEvent;
    public static void TriggerEnemyCollision(GameObject enemy)
    {
        if (EnemyCollisionEvent != null)
            EnemyCollisionEvent(enemy);
    }
}
