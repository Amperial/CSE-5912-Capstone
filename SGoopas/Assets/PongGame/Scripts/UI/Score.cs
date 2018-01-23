using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private int count;

    public void IncrementScore()
    {
        count++;
    }

    void Start()
    {
        count = 0;
        Text scoreText = this.GetComponent<Text>();
        UpdateScore(scoreText, count);
    }

    void Update()
    {
        Text scoreText = this.GetComponent<Text>();
        UpdateScore(scoreText, count);

    }

    void UpdateScore(Text text, int score)
    {
        text.text = "Score: " + score.ToString();
    }
}
