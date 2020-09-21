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
    private void Start()
    {
        currentNutHeight = 1;
        nutSpacing = .125f;
        nutAmount = 0;
        nutCount = 0;
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.L))
       {
           SpawnNut(nutPrefabs[0]);
       }
    }

    public void SpawnNut(GameObject nutType)
    {
        nutAmount--;
        nutCount++;
        if (nutCount == 16 || nutCount == 31 || nutCount == 46 || nutCount == 63 || nutCount == 80 || nutCount == 97 || nutCount == 114 || nutCount == 131)
        {
            currentNutHeight--;
            nutAmount = -1;
        }
        if (nutCount == 147)
        {
            SceneManager.LoadScene(0);
        }
        currentPos = new Vector3(initialPos.x * nutSpacing * nutAmount - 6.8f, initialPos.y * nutSpacing * currentNutHeight + 4.5f,initialPos.z);
        if (nutType == nutPrefabs[0])
        {
            Instantiate(nutPrefabs[0], currentPos, Quaternion.Euler(initialRot));
        }

        if (nutType == nutPrefabs[1])
        {
            Instantiate(nutPrefabs[1], currentPos, Quaternion.Euler(initialRot));
        }

        if (nutType == nutPrefabs[2])
        {
            Instantiate(nutPrefabs[2], currentPos, Quaternion.Euler(initialRot));
        }
    }
}
