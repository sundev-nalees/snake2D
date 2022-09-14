using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score = 0;

    public static Score instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scoreForGainer;
    
    private void Start()
    { 

        scoreText.text = gameObject.tag + "-" + score;
    }

    private void Awake()
    {
        instance = this;
    }

    public void IncreaseScore()
    {
        score += scoreForGainer;
        scoreText.text = gameObject.tag + "-" + score;
    }
    public void DecreseScore()
    {
        score -= scoreForGainer;
        scoreText.text = gameObject.tag + "-" + score;
    }
    public void SetScoreForGainer(int score)
    {
        scoreForGainer = score;
    }
    public int GetScoreForGainer()
    {
        return scoreForGainer;
    }

    public int GetActiveScore()
    {
        return score;
    }
}
