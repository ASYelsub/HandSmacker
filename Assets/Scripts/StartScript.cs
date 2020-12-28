using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private Vector2 creditsOnMin;
    private Vector2 creditsOffMin;
    private Vector2 optionsOnMin;
    private Vector2 optionsOffMin;
    private Vector2 creditsOnMax;
    private Vector2 creditsOffMax;
    private Vector2 optionsOnMax;
    private Vector2 optionsOffMax;

    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private AudioClip iceCrack;

    private void Start()
    {
        creditsOnMax = new Vector2(creditsPanel.GetComponent<RectTransform>().offsetMax.x, 46f);
        creditsOnMin = new Vector2(creditsPanel.GetComponent<RectTransform>().offsetMin.x,-46f);
        creditsOffMax = new Vector2(creditsPanel.GetComponent<RectTransform>().offsetMax.x, 511f);
        creditsOffMin = new Vector2(creditsPanel.GetComponent<RectTransform>().offsetMin.x,-511f);
    }

    /* Specifically on computer mode
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }*/


    public void StartGame()
    {
        SceneManager.LoadScene(1);
        gameObject.GetComponent<AudioSource>().PlayOneShot(iceCrack);
    }
}
