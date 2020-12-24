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

    private float nutSpacing;
    private int currentNutHeight;

    private float nutXVal = 6.8f;
    private float nutYVal = 4.5f;

    private float screenWidth;
    private float screenHeight;

    [SerializeField]
    private Camera cam;

    private Vector3 screenmeasure;
    private Vector3 worldPos;

    private GameObject[] nutPool = new GameObject[100];
    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        Debug.Log("screenWidth: " + screenWidth + " screenHeight: " + screenHeight);
        currentNutHeight = 1;
        nutSpacing = .125f;
        nutCount = 0;
    }

    void NutPool()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {

            }
        }
    }
    private void Update()
    {
       if (Input.GetKey(KeyCode.L))
       {
           TurnNutOn(0);
       }
    }

    public void TurnNutOn(int tempInt)
    {
        nutCount++;
        
        
        Instantiate(nutPrefabs[tempInt], currentPos, Quaternion.Euler(initialRot));
    }
    
}
