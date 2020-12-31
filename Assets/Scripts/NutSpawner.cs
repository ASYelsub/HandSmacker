using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

//This script spawns nuts if there aren't the max amount of nuts on screen.

public class NutSpawner : MonoBehaviour
{
    [Header("Nut Prefabs")]
    [SerializeField]
    private GameObject almond, cashew, pecan;

    private GameObject nutSpawner;

    private Vector3 nutSpawnTransform = new Vector3(0,0,0);
    
    //plug this into activeNuts
    [SerializeField]
    private int maxAmountOfNutsOnScreen;

    [SerializeField]
    private Menu menu;

    public int amountOfNutsOnScreen;
    private bool nutsMaxed;

    private float timer;

    private void Start()
    {
        nutSpawner = gameObject;
        nutsMaxed = false;
    }

    private void FixedUpdate()
    {
        if(!menu.menuIsMoving && !menu.menuOn)
        {
            if (amountOfNutsOnScreen >= maxAmountOfNutsOnScreen)
            {
                nutsMaxed = true;
            }
            if (!nutsMaxed)
            {
                timer += Time.deltaTime;
                if (timer >= 2f)
                {
                    float randomizer = UnityEngine.Random.Range(0f, 3f);
                    if (randomizer >= 2f)
                    {
                        Instantiate(almond, nutSpawner.transform, false);
                        
                    }
                    else if (randomizer < 2f && randomizer >= 1f)
                    {
                        Instantiate(cashew, nutSpawner.transform, false);
                    }
                    else
                    {
                        Instantiate(pecan, nutSpawner.transform, false);
                    }
                    IncreaseNutCount();

                    timer = 0;
                }

            }
            //        print(nutsMaxed);
        }

    }

    private void IncreaseNutCount()
    {
        amountOfNutsOnScreen++;
    }

    public void DecreaseNutCount()
    {
        amountOfNutsOnScreen--;
        nutsMaxed = false;
//        print(maxAmountOfNutsOnScreen);
    }
}
