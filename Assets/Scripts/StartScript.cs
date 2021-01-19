﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField]private Vector3 creditsOnPos;
    [SerializeField]private Vector3 creditsOffPos;

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

    public static int currentScene;

    
    private void Start()
    {
        currentScene = 0;
        DontDestroyOnLoad(gameObject);
        optionsOn = false;
        creditsOn = false;
        menuIsMoving = false;
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
        StartCoroutine(soundWait());
    }

    public IEnumerator soundWait()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(iceCrack);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(1);
        currentScene = 1;
        yield return null;
    }

    public void startCreditFunc(bool creditsOn)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(iceCrack);
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
        gameObject.GetComponent<AudioSource>().PlayOneShot(iceCrack);
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
            hammers[0].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOnPos[0], hammerOffPos[0], timer);
            hammers[1].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOnPos[1], hammerOffPos[1], timer);
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
            hammers[0].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOffPos[0], hammerOnPos[0], timer);
            hammers[1].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOffPos[1], hammerOnPos[1], timer);
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
            hammers[0].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOnPos[0], hammerOffPos[0], timer);
            hammers[1].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOnPos[1], hammerOffPos[1], timer);
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
            hammers[0].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOffPos[0], hammerOnPos[0], timer);
            hammers[1].GetComponent<Transform>().localPosition = Vector3.Lerp(hammerOffPos[1], hammerOnPos[1], timer);
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        optionsOn = false;
        yield return null;
    }
}
