using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerMovement : MonoBehaviour
{
    private Menu menu;
    private ConveyerSpawner conveyerSpawner;
    [SerializeField]
    private Vector3 conveyerMoveVector;
    private bool corRunning;
    void Start()
    {
        conveyerSpawner = FindObjectOfType<ConveyerSpawner>();
        menu = FindObjectOfType<Menu>();
        corRunning = false;
    }

    private void FixedUpdate()
    {
        if(!menu.menuIsMoving && !menu.menuOn)
        {
            if (this.transform.position.x <= conveyerSpawner.despawnPos.x && corRunning == false)
            {
                StartCoroutine(destroyWait());
            }else if(this.transform.position.x <= conveyerSpawner.despawnPos.x && corRunning == true)
            {
                gameObject.transform.localPosition = gameObject.transform.localPosition + conveyerMoveVector;
            }
            else
            {
                gameObject.transform.localPosition = gameObject.transform.localPosition + conveyerMoveVector;
            }
        }
    }

    IEnumerator destroyWait()
    {
        corRunning = true;
        conveyerSpawner.SpawnNextConveyer();
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        corRunning = false;
        yield return null;
    }
}
