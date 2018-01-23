using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private int count;

    private int savedScore;

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
    //   private int countLeft;
    //   private int countRight;
    //   public void IncrementLeftScore()
    //   {
    //       countLeft++;
    //   }
    //   public void IncrementRightScore()
    //   {
    //       countRight++;
    //   }
    //// Use this for initialization
    //void Start () {
    //       countLeft = 0;
    //       countRight = 0;
    //}

    //// Update is called once per frame
    //void Update () {
    //       Text[] texts = this.GetComponents<Text>();
    //       foreach(Text text in texts)
    //       {
    //           if (text.name.Equals("Left Score")){
    //               UpdateScore(text, countLeft);
    //           } else if (text.name.Equals("Right Score"))
    //           {
    //               UpdateScore(text, countRight);
    //           }
    //       }
    //}

    void Save()
    {
        savedScore = count;
    }
    void Load()
    {
        count = savedScore;
    }
    void UpdateScore(Text text, int score)
    {
        text.text = "Score: " + score.ToString();
    }
}
