using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Misc : MonoBehaviour
{
    public static Misc instance;

    public Text scoreText;
    public static int scoreCount;
    public Text hiScoreText;
    public static int hiScoreCount;

    private void Awake()
    {
        instance = this;
        scoreText.text = "Score: " + scoreCount;
        hiScoreText.text = "High Score: " + hiScoreCount;
    }

    void start ()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            hiScoreCount = PlayerPrefs.GetInt("HighScore");
        }
    }
        public void Update()
    {
        if(scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            PlayerPrefs.SetInt("HighScore", hiScoreCount);
        }

    }
    public void addPoints()
    {
        scoreCount++;
        scoreText.text = "Score: " + scoreCount;
        hiScoreText.text = "High Score: " + hiScoreCount;
    }
    public void resetScore()
    {
        scoreCount = 0;
    }

}
