using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public TextMeshProUGUI text;
    public int score;

    private void Update()
    {
        text.text = score.ToString();
    }

    public void AddScore(int value)
    {
        score += value;
    }
}
