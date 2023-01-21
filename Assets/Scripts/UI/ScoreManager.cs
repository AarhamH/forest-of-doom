using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int scoreDisplay = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Gold: " + scoreDisplay.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreDisplay.ToString();
    }
}
