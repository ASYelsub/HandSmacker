using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private Vector3 creditsOnPos;
    private Vector3 creditsOffPos;

    bool creditsOn;
    bool menuIsMoving;

    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private AudioClip iceCrack;

    private void Start()
    {
        creditsOn = false;
        menuIsMoving = false;
        creditsOffPos = new Vector3(0, -484);
        creditsOnPos = new Vector3(0, -19);
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

    public void startCreditFunc(bool creditsOn)
    {
        if (!creditsOn)
        {
            StartCoroutine(MoveCreditsUp());
        }
        else
        {
            StartCoroutine(MoveCreditsDown());
        }

    }

    private IEnumerator MoveCreditsUp()
    {
        float timer = 0;
        while (timer < 1)
        {
            creditsPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(creditsOffPos, creditsOnPos, timer);
            Debug.Log("HELLO");
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        creditsOn = true;
        yield return null;
    }

    private IEnumerator MoveCreditsDown()
    {
        float timer = 0;
        while (timer < 1)
        {
            creditsPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(creditsOnPos, creditsOffPos, timer);
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        creditsOn = false;
        yield return null;
    }
}
