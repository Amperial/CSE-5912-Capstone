using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : MonoBehaviour {
    private bool forwards = true;
    private int point = 0;
    Vector3[] points;

    public float speed;
    [HideInInspector]
    public bool move = false;

	// Use this for initialization
	void Start () {
        RecalculatePath();
	}
    void Move()
    {
        float maxDist = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, points[point], maxDist);

        if (forwards)
        {
            if (Vector3.Distance(transform.position, points[point]) < 0.0001)
                point++;
            if (point == points.Length)
            {
                forwards = false;
                point--;
            }

        }
        else
        {
            if (Vector3.Distance(transform.position, points[point]) < 0.0001)
                point--;
            if (point == -1)
            {
                forwards = true;
                point++;
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (move)
            Move();
	}

    void SwitchTo3D(Cancellable cancellable)
    {
        cancellable.PerformCancellable(() => move = false, () => move = true);
    }
    void SwitchTo2D(Cancellable cancellable)
    {
        cancellable.PerformCancellable(() => move = true, () => move = false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == MainObjectContainer.Instance.Player2D)
        {
            PlayerDeathHandler.TriggerPlayerDeath();
            EnemyCollisionHandler.TriggerEnemyCollision(gameObject);
        }
    }

    public void RecalculatePath()
    {
        points = new Vector3[transform.childCount + 1];
        points[0] = transform.position;
        for (int i = 0; i < transform.childCount; i++)
        {
            points[i + 1] = transform.GetChild(i).position;
        }
    }

}
