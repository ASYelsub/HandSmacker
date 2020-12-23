using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    Transform menuTransform;
    
    [HideInInspector]public bool menuOn;
    [HideInInspector]public bool menuIsMoving;
    Vector3 menuOnPos;
    Vector3 menuOffPos;
    void Start()
    {
        menuOnPos = new Vector3(0f, -0.047f, 0.69166f);
        //scale of 0.126693,0.126693,0.08172395
        menuOffPos = new Vector3(0f, -0.84f, 0.69166f);
        menuOn = false;
        menuIsMoving = false;
        menuTransform = gameObject.GetComponent<Transform>();
        menuTransform.localPosition = menuOffPos;
    }

    public void TurnMenuOnOff(bool currentMenuState)
    {
        if (!currentMenuState)
        {
            StartCoroutine(MoveMenuUp());         
        }
        else if (currentMenuState)
        {
            StartCoroutine(MoveMenuDown());
        }
    }

    private IEnumerator MoveMenuUp()
    {
        float timer = 0;
        while (timer < 1)
        {
            menuTransform.localPosition = Vector3.Lerp(menuOffPos, menuOnPos, timer);
            timer = timer + .01f;
            Debug.Log(timer);
            yield return null;
        }
        menuIsMoving = false;
        menuOn = true;
        yield return null;
    }

    private IEnumerator MoveMenuDown()
    {
        float timer = 0;
        while (timer < 1)
        {
            menuTransform.localPosition = Vector3.Lerp(menuOnPos, menuOffPos, timer);
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        menuOn = false;
        yield return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuIsMoving)
        {
            TurnMenuOnOff(menuOn);
            menuIsMoving = true;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
