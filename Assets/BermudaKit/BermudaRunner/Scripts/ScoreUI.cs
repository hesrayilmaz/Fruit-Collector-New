using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int score;

    private void Start()
    {
        score = 0;
    }
    private void Update()
    {
        scoreText.text = "" + score;
    }

}
