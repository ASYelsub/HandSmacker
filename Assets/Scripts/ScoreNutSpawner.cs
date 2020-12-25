using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreNutSpawner : MonoBehaviour
{
    /*Pseudocode:
     * First, we are going to "show" 72 nuts 3 times. this is the outer for-loop, for the 
     * first three numbers the nuts are going to be in a 9x8 square on screen
     * above the conveyer belt.
     * 
     * To do this, we are going to need to get the screen height and width.
     * The score nuts are most affected by changing aspect ratio out of all things in the game.
     * 
     * Things that change depending on aspect ratio:
     * -starting position of nut at the top most left point on the screen
     * -the amount of space both in x and y directions of nuts after starting nut
     * 
     * 
     * if the screen is wider, the starting position should be more negative in the x
     * if the screen is narrower, the starting position should be closer to zero in the x
     * 
     * do not need to worry about the starting point of the y.
     */









    private float tempNutNum;


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

    private float widthSpacing;
    private float heightSpacing;
    private int currentNutHeight;

    private float screenWidth;
    private float screenHeight;

    private GameObject almondObject;
    private GameObject cashewObject;
    private GameObject pecanObject;

    [SerializeField]
    private GameObject nutScoreObject;

    [SerializeField]
    private Camera cam;

    private Vector3 screenmeasure;
    private Vector3 worldPos;
    private float nutZ;

    //Most of these are for specifically 16x9
    [Header("Tuning Variables")]
    [SerializeField] private float xNutAmount;
    [SerializeField] private float yNutAmount;
    [SerializeField] private float spaceAmount; //for the screenspecfic one
    [SerializeField] private float nutX;
    [SerializeField] private float nutY;
    [SerializeField] private float spaceX;
    [SerializeField] private float spaceY;

    private float initialYNutAmount, initialXNutAmount, initialNutZ, initialNutX,initialNutY;
    //Thesea are all screen specific ones
    private float widthRatio, heightRatio, nutSpacerWidth, nutSpacerHeight, nutStartX, nutStartY;
    private Vector3 nutPos;
    public float spacer;

    private List<GameObject> nutPool = new List<GameObject>();
    
    private void Start()
    {

        nutZ = -3f;

        initialYNutAmount = yNutAmount;
        initialXNutAmount = xNutAmount;
        initialNutX = nutX;
        initialNutY = nutY;
        initialNutZ = nutZ;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        heightRatio = (screenHeight / screenWidth);
        widthRatio = (screenWidth / screenHeight);
        Debug.Log(" widthRatio: " + widthRatio + " heightRatio: " + heightRatio + " screenWidth: " + screenWidth + " screenHeight: " + screenHeight);
        nutCount = 0;
      


        nutSpacerWidth = (screenWidth / (xNutAmount * spaceAmount)) * widthRatio;
        nutSpacerHeight = (screenHeight / (yNutAmount * spaceAmount) * widthRatio);

        //I THINK this is correct??? for aspect ratio stuff
        //nutStartX = widthRatio * nutX;
        //nutStartY = heightRatio * nutY;

        NutPool();
    }

    void NutPool()
    {

        //Three nuts on main screen
        for(int q = 0; q < 3; q++)
        {
            if(q == 1)
            {
                yNutAmount = initialYNutAmount;
                xNutAmount = initialXNutAmount;
                nutZ = initialNutZ + .1f;
                nutX = initialNutX + spaceX / 2;
                nutY = initialNutY + spaceY / 2;
            }
            else if (q == 2)
            {
                yNutAmount = initialYNutAmount;
                xNutAmount = initialXNutAmount;
                nutZ = initialNutZ - .1f;
                nutX = initialNutX + spaceX / 2 - .4f;
                nutY = initialNutY + spaceY / 2 - .2f;
            }
            else
            {
                yNutAmount = initialYNutAmount;
                xNutAmount = initialXNutAmount;
            }
            for (int i = 0; i < yNutAmount; i++)
            {
                for (int j = 0; j < xNutAmount; j++)
                {
                    SpawnNut(i, j);
                }
            }
            
        }
        //First layer above main square
        for(int q = 0; q < 3; q++)
        {
            if (q == 1)
            {
                nutZ = initialNutZ + .1f;
                nutX = initialNutX + spaceX + (spaceX / 2);
                nutY = initialNutY + spaceY + (spaceY / 2);
            }
            else if (q == 2)
            {
                nutZ = initialNutZ - .1f;
                nutX = initialNutX + spaceX + (spaceX / 2) - .4f;
                nutY = initialNutY + spaceY + (spaceY / 2) - .2f;
            }
            else
            {
                nutX = initialNutX + (spaceX);
                nutY = initialNutY + (spaceY);
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < xNutAmount; j++)
                {
                    //Debug.Log("THIS HAPPENED");
                    SpawnNut(i, j);              
                }
            }
        }

        //First layer below main square
        for (int q = 0; q < 3; q++)
        {
            if (q == 1)
            {
                nutZ = initialNutZ + .1f;
                nutX = initialNutX - spaceX + (spaceX / 2);
                nutY = initialNutY - initialYNutAmount*spaceY + (spaceY / 2);
            }
            else if (q == 2)
            {
                nutZ = initialNutZ - .1f;
                nutX = initialNutX - spaceX + (spaceX / 2) - .4f;
                nutY = initialNutY - initialYNutAmount*spaceY + (spaceY / 2) - .2f;
            }
            else
            {
                nutX = initialNutX - (spaceX);
                nutY = initialNutY - initialYNutAmount*(spaceY);
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < xNutAmount; j++)
                {
                    //Debug.Log("THIS HAPPENED");
                    SpawnNut(i, j);
                }
            }
        }

        //Second layer above main square
        for (int q = 0; q < 3; q++)
        {
            if (q == 1)
            {
                nutZ = initialNutZ + .1f;
                nutX = initialNutX + 2*spaceX + (spaceX / 2);
                nutY = initialNutY + 2*spaceY + (spaceY / 2);
            }
            else if (q == 2)
            {
                nutZ = initialNutZ - .1f;
                nutX = initialNutX + 2*spaceX + (spaceX / 2) - .4f;
                nutY = initialNutY + 2*spaceY + (spaceY / 2) - .2f;
            }
            else
            {
                nutX = initialNutX + 2*(spaceX);
                nutY = initialNutY + 2*(spaceY);
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < xNutAmount + 1; j++)
                {
                    //Debug.Log("THIS HAPPENED");
                    SpawnNut(i, j);
                }
            }
        }

        //Second layer below main square
        for (int q = 0; q < 3; q++)
        {
            if (q == 1)
            {
                nutZ = initialNutZ + .1f;
                nutX = initialNutX + (spaceX / 2) - spaceX;
                nutY = initialNutY - initialYNutAmount * spaceY + (spaceY / 2) - spaceY;
            }
            else if (q == 2)
            {
                nutZ = initialNutZ - .1f;
                nutX = initialNutX - spaceX + (spaceX / 2) - .4f;
                nutY = initialNutY - initialYNutAmount * spaceY + (spaceY / 2) - .2f - spaceY;
            }
            else
            {
                nutX = initialNutX - spaceX;
                nutY = initialNutY - initialYNutAmount * (spaceY) - spaceY;
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < xNutAmount + 1; j++)
                {
                    //Debug.Log("THIS HAPPENED");
                    SpawnNut(i, j);
                }
            }
        }

        //Second layer above main square
        for (int q = 0; q < 3; q++)
        {
            if (q == 1)
            {
                nutZ = initialNutZ + .1f;
                nutX = initialNutX + 3 * spaceX + (spaceX / 2);
                nutY = initialNutY + 3 * spaceY + (spaceY / 2);
            }
            else if (q == 2)
            {
                nutZ = initialNutZ - .1f;
                nutX = initialNutX + 3 * spaceX + (spaceX / 2) - .4f;
                nutY = initialNutY + 3 * spaceY + (spaceY / 2) - .2f;
            }
            else
            {
                nutX = initialNutX + 3 * (spaceX);
                nutY = initialNutY + 3 * (spaceY);
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < xNutAmount + 3; j++)
                {
                    //Debug.Log("THIS HAPPENED");
                    SpawnNut(i, j);
                }
            }
        }

        //Third layer below main square
        for (int q = 0; q < 3; q++)
        {
            if (q == 1)
            {
                nutZ = initialNutZ + .1f;
                nutX = initialNutX + (spaceX / 2);
                nutY = initialNutY - initialYNutAmount * spaceY + (spaceY / 2) - (2*spaceY);
            }
            else if (q == 2)
            {
                nutZ = initialNutZ - .1f;
                nutX = initialNutX + (spaceX / 2) - .4f;
                nutY = initialNutY - initialYNutAmount * spaceY + (spaceY / 2) - .2f -(2*spaceY);
            }
            else
            {
                nutX = initialNutX;
                nutY = initialNutY - initialYNutAmount * (spaceY) - (2*spaceY);
            }
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < xNutAmount + 3; j++)
                {
                    //Debug.Log("THIS HAPPENED");
                    SpawnNut(i, j);
                }
            }
        }
    }

    void SpawnNut(int i, int j)
    {
        nutPos = new Vector3(j * spaceX - nutX,
                                         -i * spaceY + nutY,
                                         nutZ);
        GameObject tempNut = Instantiate(nutScoreObject, nutPos, Quaternion.identity, gameObject.transform);
        nutPool.Add(tempNut);
        MeshRenderer[] meshrends = tempNut.GetComponentsInChildren<MeshRenderer>();
        //Added in later when "nut art" is done.
        for(int k = 0; k < meshrends.Length; k++)
        {
            meshrends[k].enabled = false;
        }
    }
    private void Update()
    {
        if (nutCount < nutPool.Count)
        {
            if (Input.GetKey(KeyCode.L))
            {
                TurnNutOn(0);
            }
        }
        
    }

    public void TurnNutOn(int tempInt)
    {
        nutCount++;

        MeshRenderer[] meshrends = nutPool[nutCount - 1].GetComponentsInChildren<MeshRenderer>();
        meshrends[tempInt].enabled = true;
        if(nutCount == (yNutAmount * xNutAmount) ||
            nutCount == 2*(yNutAmount * xNutAmount)) //||
            //nutCount == 3 * (yNutAmount * xNutAmount))
        {
            MoveEverythingBack(0);
            //Debug.Log("Happened");
        }

        if(nutCount == 3*(yNutAmount * xNutAmount) + (xNutAmount) ||
            nutCount == 3*(yNutAmount * xNutAmount) + 2*xNutAmount)
        {
            MoveEverythingBack(0);
        }

        if(nutCount == 3*(yNutAmount * xNutAmount) + (4*xNutAmount) ||
            nutCount == 3*(yNutAmount * xNutAmount) + (5*xNutAmount))
        {
            MoveEverythingBack(0);
        }
        if(nutCount == 3*(yNutAmount * xNutAmount) + (6*xNutAmount) + 2*(xNutAmount + 1) ||
            nutCount == 3*(yNutAmount * xNutAmount) + (6*xNutAmount) + 3*(xNutAmount + 1))
        {
            MoveEverythingBack(0);
            //Debug.Log("THIS HAPPENED");
        }
        //second row bottom
        if (nutCount == 3 * (yNutAmount * xNutAmount) + (6 * xNutAmount) + 5 * (xNutAmount + 1) ||
            nutCount == 3 * (yNutAmount * xNutAmount) + (6 * xNutAmount) + 6 * (xNutAmount + 1))
        {
            MoveEverythingBack(1);
            //Debug.Log("THIS HAPPENED");
        }
        //Third row top
        if (nutCount == 3 * (yNutAmount * xNutAmount) + (6 * xNutAmount) + 6 * (xNutAmount + 1) + 2 * (xNutAmount + 3) ||
            nutCount == 3 * (yNutAmount * xNutAmount) + (6 * xNutAmount) + 6 * (xNutAmount + 1) + 3 * (xNutAmount + 3))
        {
            MoveEverythingBack(0);
            //Debug.Log("THIS HAPPENED");
        }
        if (nutCount == 3 * (yNutAmount * xNutAmount) + (6 * xNutAmount) + 6 * (xNutAmount + 1) + 5 * (xNutAmount + 3) ||
            nutCount == 3 * (yNutAmount * xNutAmount) + (6 * xNutAmount) + 6 * (xNutAmount + 1) + 6 * (xNutAmount + 3))
        {
            MoveEverythingBack(0);
            //Debug.Log("THIS HAPPENED");
        }
    }

    public void MoveEverythingBack(int j)
    {
        if(j == 0)
        {
            gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, 0, .5f);
        }else if(j == 1)
        {
            gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + new Vector3(0, .2f, .5f);
        }

    }
    
}
