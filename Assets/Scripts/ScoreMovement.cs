using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMovement : MonoBehaviour
{
    private RectTransform scoreTextTransform;
    private Text scoreTextText;

    [SerializeField]
    private Vector3[] textPosition;

    private int textPosInt;


    private void Start()
    {
        scoreTextTransform = gameObject.GetComponent<RectTransform>();
        scoreTextText = gameObject.GetComponent<Text>();
        scoreTextTransform.anchoredPosition = textPosition[0];
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateScore(1);
        }
    }*/

    public void UpdateScore(int scoreAdd)
    {
        scoreTextText.text = scoreAdd.ToString();

        if (textPosInt < textPosition.Length - 1)
        {
            textPosInt++;
        }
        else
        {
            textPosInt = 0;
        }
        scoreTextTransform.anchoredPosition = textPosition[textPosInt];
        
    }
}
