using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    private Text scoreText;
    private int score;
    public enum ScoreType { Hit = 5, Kill = 20, HeadshotKill = 40 };

    public int Score
    {
        get { return score; }
        set
        {

            score = value;

            if (score < 0)
            {
                score = 0;
            }

            scoreText.text = score + "";
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            scoreText = GetComponent<Text>();
            scoreText.text = "0";
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
