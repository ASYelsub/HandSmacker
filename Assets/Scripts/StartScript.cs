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
    bool optionsOn;
    bool menuIsMoving;
    [SerializeField]
    private Vector3[] hammerOnPos;
    [SerializeField]
    private Vector3[] hammerOffPos;

    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private GameObject[] hammers;
    [SerializeField]
    private AudioClip iceCrack;

    private void Start()
    {
        optionsOn = false;
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

    public void startOptionFunc(bool optionsOn)
    {
        if (!optionsOn)
        {
            StartCoroutine(MoveOptionsUp());
        }
        else
        {
            StartCoroutine(MoveOptionsDown());
        }
    }

    private IEnumerator MoveCreditsUp()
    {
        float timer = 0;
        while (timer < 1)
        {
            creditsPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(creditsOffPos, creditsOnPos, timer);
            hammers[0].GetComponent<Transform>().position = Vector3.Lerp(hammerOnPos[0], hammerOffPos[0], timer);
            hammers[1].GetComponent<Transform>().position = Vector3.Lerp(hammerOnPos[1], hammerOffPos[1], timer);
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
            hammers[0].GetComponent<Transform>().position = Vector3.Lerp(hammerOffPos[0], hammerOnPos[0], timer);
            hammers[1].GetComponent<Transform>().position = Vector3.Lerp(hammerOffPos[1], hammerOnPos[1], timer);
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        creditsOn = false;
        yield return null;
    }

    private IEnumerator MoveOptionsUp()
    {
        float timer = 0;
        while (timer < 1)
        {
            optionsPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(creditsOffPos, creditsOnPos, timer);
            hammers[0].GetComponent<Transform>().position = Vector3.Lerp(hammerOnPos[0], hammerOffPos[0], timer);
            hammers[1].GetComponent<Transform>().position = Vector3.Lerp(hammerOnPos[1], hammerOffPos[1], timer);
            Debug.Log("HELLO");
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        optionsOn = true;
        yield return null;
    }
    private IEnumerator MoveOptionsDown()
    {
        float timer = 0;
        while (timer < 1)
        {
            optionsPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(creditsOnPos, creditsOffPos, timer);
            hammers[0].GetComponent<Transform>().position = Vector3.Lerp(hammerOffPos[0], hammerOnPos[0], timer);
            hammers[1].GetComponent<Transform>().position = Vector3.Lerp(hammerOffPos[1], hammerOnPos[1], timer);
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        optionsOn = false;
        yield return null;
    }
}
