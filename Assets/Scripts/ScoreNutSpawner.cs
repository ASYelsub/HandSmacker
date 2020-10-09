using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreNutSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] nutPrefabs;

    [SerializeField] 
    private GameObject[] insertedNutTypes;


    [SerializeField]private Vector3 initialPos;
    [SerializeField]private Vector3 initialRot;
    private Vector3 currentPos;

    private int nutAmountLength = 15;
    private int nutAmountHeight;

    private int nutCount;

    private int nutAmount;
    private float nutSpacing;
    private int currentNutHeight;

    private float nutXVal = 6.8f;
    private float nutYVal = 4.5f;
    private void Start()
    {
        currentNutHeight = 1;
        nutSpacing = .125f;
        nutAmount = 0;
        nutCount = 0;
    }

    private void Update()
    {
       if (Input.GetKey(KeyCode.L))
       {
           SpawnNut(0);
       }
    }

    public void SpawnNut(int tempInt)
    {
        nutAmount--;
        nutCount++;
        switch (nutCount)
        {
            case 18:
            case 35:
            case 52:
            case 69:
            case 86:
            case 103:
            case 120:
            case 137:
            case 154:
                currentNutHeight--;
                nutAmount = -1;
                break;
            case 171:
                currentNutHeight = 1;
                nutAmount = -2;
                nutXVal += .4f;
                nutYVal += .3f;
                initialPos.z += .1f;
                //initialPos.x -= .5f;
                //This only happens in the web version of the game
                //SceneManager.LoadScene(2);
                break;
            case 187:
            case 203:
            case 219: 
            case 235:
            case 251: 
            case 267: //need to finish this
                currentNutHeight--;
                nutAmount = -2;
                break;
        }
        //nutXVal = 6.8f at first
        //nutYVal = 4.5f at first
        currentPos = new Vector3(initialPos.x * nutSpacing * nutAmount - nutXVal, initialPos.y * nutSpacing * currentNutHeight + nutYVal,initialPos.z);
        Instantiate(nutPrefabs[tempInt], currentPos, Quaternion.Euler(initialRot));
    }
}
