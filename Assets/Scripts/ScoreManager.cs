using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();
        PlayerPrefs.SetInt("score", 0);
    }

    public void AddPoint()
    {
        score += 1;
        PlayerPrefs.SetInt("score", score);
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        Debug.Log("+1 Score! Current Score: " + score);
    }
}
