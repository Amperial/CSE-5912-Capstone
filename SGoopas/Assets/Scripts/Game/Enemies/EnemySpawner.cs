using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    Vector3[] points;
    public GameObject enemyPrefab;
    private FloatingEnemy enemyScript;
    // Use this for initialization
    void Start () {
        points = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            points[i] = transform.GetChild(i).position;
        }
    }

    void MakeEnemy()
    {
        if(enemyScript == null)
        {
            GameObject enemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            enemy.transform.parent = transform.parent;
            foreach (Vector3 point in points)
            {
                GameObject temp = new GameObject();
                temp.transform.position = point;
                temp.transform.parent = enemy.transform;
            }
            enemyScript = enemy.GetComponent<FloatingEnemy>();
            enemyScript.RecalculatePath();
        }
    }

    void Swap2D()
    {
        MakeEnemy();
        enemyScript.move = true;
    }

    void Cancel2D()
    {
        enemyScript.move = false;
    }

    void SwitchTo3D(Cancellable cancellable)
    {
        
    }
    void SwitchTo2D(Cancellable cancellable)
    {
        cancellable.PerformCancellable(Swap2D, Cancel2D);
    }
}
