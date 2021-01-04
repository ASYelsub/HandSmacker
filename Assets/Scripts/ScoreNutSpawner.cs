using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreNutSpawner : MonoBehaviour
{
    /*Pseudocode:
     * round - sideBuff - topbottombuff- pos
    1 - 2 - 1 - 0.03,0.4,-0.4
    2 - 5 - 2 - 0.03,0.59,1.34
    3 - 6 - 3 - 0.03,0.88,3
    4 - 10 - 4 - 0.03,1.16,5.4
    5 - 12 - 5 - 0.08,1.34,7.19
    6 - 14 - 6 - 0.08,1.61,8.76
    7 - 16 - 7 - -0.15,1.82,10.6
    8 - 18 - 8 - -0.62,2.2,12.69
    9 - 20 - 9 - -0.62,2.38,14.49
    10 - NA - 10 - -0.62,2.83,16.46
    11 - NA - 11 - 1.2,3.44,18.08
    12 - NA - NA - 1.73,4.18,21.32
     * Round 0:
     * Just spawn as a square or whatever
     * at the end of turning 0 on, move out to "round 1"
     * Round 1:
     */

    private Vector3[] roundPositions = new Vector3[13];
    private int amountAddedToSides;
    private int amountAddedToTopAndBottom;

    [HideInInspector]public int currentNutRound;
    [HideInInspector]public int[] amountOfNutPerRound; //hook this variable up


    [SerializeField]
    private GameObject[] nutPrefabs;

    [SerializeField] 
    private GameObject[] insertedNutTypes;


    [SerializeField]private Vector3 initialPos;
    [SerializeField]private Vector3 initialRot;


    private int nutCount;

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
    [SerializeField] private int xNutAmountSquare;
    [SerializeField] private int yNutAmountSquare;
    [SerializeField] private float spaceAmount; //for the screenspecfic one
    [SerializeField] private float nutX;
    [SerializeField] private float nutY;
    [SerializeField] private float spaceX;
    [SerializeField] private float spaceY;

    private int initialYNutAmount, initialXNutAmount;
    private float initialNutZ, initialNutX,initialNutY;
    //Thesea are all screen specific ones
    private float widthRatio, heightRatio, nutSpacerWidth, nutSpacerHeight, nutStartX, nutStartY;
    private Vector3 nutPos;
    public float spacer;

    private int compiledNutPerRound;

    private List<GameObject> nutPool = new List<GameObject>();
    
    private void Start()
    {
        compiledNutPerRound = 0;
        amountOfNutPerRound = new int[10];
        roundPositions[0] = new Vector3(gameObject.GetComponent<Transform>().position.x,gameObject.GetComponent<Transform>().position.y,gameObject.GetComponent<Transform>().position.z);
        roundPositions[1] = new Vector3(0.03f,0.4f,-0.4f);
        roundPositions[2] = new Vector3(0.03f,0.59f,1.34f);
        roundPositions[3] = new Vector3(0.03f,0.88f,3f);
        roundPositions[4] = new Vector3(0.03f,1.16f,5.4f);
        roundPositions[5] = new Vector3(0.08f,1.34f,7.19f);
        roundPositions[6] = new Vector3(0.08f,1.7f,8.76f);
        roundPositions[7] = new Vector3(-0.15f,1.95f,10.9f);
        roundPositions[8] = new Vector3(-0.62f,2.2f,12.69f);
        roundPositions[9] = new Vector3(-0.62f,2.38f,14.49f);
        roundPositions[10] = new Vector3(-0.62f,2.83f,16.46f);
        roundPositions[11] = new Vector3(1.2f,3.44f,18.08f);
        roundPositions[12] = new Vector3(1.73f,4.18f,21.32f);

        nutZ = 0f;

        currentNutRound = 0;
        amountOfNutPerRound[0] = xNutAmountSquare * yNutAmountSquare;

        initialYNutAmount = yNutAmountSquare;
        initialXNutAmount = xNutAmountSquare;
        initialNutX = nutX;
        initialNutY = nutY;
        initialNutZ = nutZ;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        heightRatio = (screenHeight / screenWidth);
        widthRatio = (screenWidth / screenHeight);
        //Debug.Log(" widthRatio: " + widthRatio + " heightRatio: " + heightRatio + " screenWidth: " + screenWidth + " screenHeight: " + screenHeight);
        nutCount = 0;
       


        nutSpacerWidth = (screenWidth / (xNutAmountSquare * spaceAmount)) * widthRatio;
        nutSpacerHeight = (screenHeight / (yNutAmountSquare * spaceAmount) * widthRatio);

        //I THINK this is correct??? for aspect ratio stuff
        //nutStartX = widthRatio * nutX;
        //nutStartY = heightRatio * nutY;

        NutPool();
    }
    

    void NutPool()
    {
        yNutAmountSquare = initialYNutAmount;
        xNutAmountSquare = initialXNutAmount;
        Round0();
        Round1();
        Round2();
        Round3();
        Round4();
        Round5();
        Round6();
        //Round 7
        //Round 8
        //Round 9
        //Round 10
        //Round 11
        //Round 12
    }
    private void Update()
    {
        //Debug.Log(nutCount +" "+ nutPool.Count);
        if (nutCount < nutPool.Count)
        {
            //Debug.Log("???");
            if (Input.GetKey(KeyCode.L))
            {
                TurnNutOn(0, currentNutRound);
              //  Debug.Log("HITTING L");
            }
        }
    }
    void Round0()
    {
        amountOfNutPerRound[0] = yNutAmountSquare * xNutAmountSquare;
        for (int i = 0; i < yNutAmountSquare; i++)
        {
            //          Debug.Log("in first for loop");
            for (int j = 0; j < xNutAmountSquare; j++)
            {
                //                Debug.Log("in second for loop");
                nutPos = new Vector3(j * spaceX - nutX,
                                         -i * spaceY + nutY,
                                         nutZ);
                newNut(nutPos);
            }
        }
    }
    void Round1()
    {
        amountOfNutPerRound[1] = 0;
        //top row: starts 3 to the left of the first original spot, stops one before first final spot on left
        //second row through 9th row: 3 new on each side
        //tenth row: 3 new on left, all new on bottom, 3 new on right

        //going down, starting on right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX*18,
                                         -i * spaceY + nutY,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + 9;
        //going left, starting at bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 18; j > 0; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX,
                                         -i * spaceY + nutY - spaceY * 8,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + 18;
        //going up, starting at left
        for (int i = 9; i > 0 ; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX,
                                         -i * spaceY + nutY + spaceY,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + 9;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            //          Debug.Log("in first for loop");
            for (int j = 0; j < xNutAmountSquare; j++)
            {
                //                Debug.Log("in second for loop");
                nutPos = new Vector3(j * spaceX - nutX - spaceX,
                                         -i * spaceY + nutY + spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + xNutAmountSquare;
        //going down, starting on right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 19,
                                         -i * spaceY + nutY,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + 9;
        //going up, starting at left
        for (int i = 10; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 2,
                                         -i * spaceY + nutY + spaceY * 2,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + 10;
        //going down, starting on right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 20,
                                         -i * spaceY + nutY,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + 9;
        //going up, starting at left
        for (int i = 10; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 3,
                                         -i * spaceY + nutY + spaceY * 2,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[1] = amountOfNutPerRound[1] + 10;
    }
    void Round2()
    {
        amountOfNutPerRound[2] = 0;
        //going down, starting on right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 21,
                                         -i * spaceY + nutY - spaceY,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[2] = amountOfNutPerRound[2] + 9;
        //going left, starting at bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 20; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX,
                                         -i * spaceY + nutY - spaceY * 9,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[2] = amountOfNutPerRound[2] + 21;
        //going up, starting at left
        for (int i = 10; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 4,
                                         -i * spaceY + nutY + spaceY * 2,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[2] = amountOfNutPerRound[2] + 10;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            //          Debug.Log("in first for loop");
            for (int j = 0; j < xNutAmountSquare - 1; j++)
            {
                //                Debug.Log("in second for loop");
                nutPos = new Vector3(j * spaceX - nutX - 4 * spaceX,
                                         -i * spaceY + nutY + 2 * spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[2] = amountOfNutPerRound[2] + xNutAmountSquare - 1;
        //going down, starting on right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 22,
                                         -i * spaceY + nutY - spaceY,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[2] = amountOfNutPerRound[2] + 9;
        //going up, starting at left
        for (int i = 11; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 5,
                                         -i * spaceY + nutY + spaceY * 3,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[2] = amountOfNutPerRound[2] + 11;
    }
    void Round3()
    {
        amountOfNutPerRound[3] = 0;
        //going down, starting on right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 23,
                                         -i * spaceY + nutY - spaceY * 2,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[3] = amountOfNutPerRound[3] + 9;
        //going left, starting at bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 21; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX,
                                         -i * spaceY + nutY - spaceY * 10,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[3] = amountOfNutPerRound[3] + 22;
        //going up, starting at left
        for (int i = 11; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 6,
                                         -i * spaceY + nutY + spaceY * 3,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[3] = amountOfNutPerRound[3] + 11;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            //          Debug.Log("in first for loop");
            for (int j = 0; j < xNutAmountSquare - 1; j++)
            {
                //                Debug.Log("in second for loop");
                nutPos = new Vector3(j * spaceX - nutX - 6 * spaceX,
                                         -i * spaceY + nutY + 3 * spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[3] = amountOfNutPerRound[3] + xNutAmountSquare - 1;
        //going down, starting on right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 24,
                                         -i * spaceY + nutY - spaceY * 2,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[3] = amountOfNutPerRound[3] + 9;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 7,
                                         -i * spaceY + nutY + spaceY * 4,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[3] = amountOfNutPerRound[3] + 12;
        
    }
    void Round4()
    {
        //going down, starting right
        amountOfNutPerRound[4] = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 25,
                                         -i * spaceY + nutY - spaceY * 3,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[4] = amountOfNutPerRound[4] + 8;
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 23; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 2,
                                         -i * spaceY + nutY - spaceY * 11,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[4] = amountOfNutPerRound[4] + 24;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 8,
                                         -i * spaceY + nutY + spaceY * 4,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[4] = amountOfNutPerRound[4] + 12;
        //going right, starting at top
         for (int i = 0; i < 1; i++)
         {
             //          Debug.Log("in first for loop");
             for (int j = 0; j < xNutAmountSquare - 2; j++)
             {
                 //                Debug.Log("in second for loop");
                 nutPos = new Vector3(j * spaceX - nutX - 8 * spaceX,
                                          -i * spaceY + nutY + 4 * spaceY,
                                          nutZ);
                 newNut(nutPos);
              //   Debug.Log("yo");
             }
         }
         amountOfNutPerRound[4] = amountOfNutPerRound[4] + xNutAmountSquare - 2;
        //going down, starting right
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 26,
                                         -i * spaceY + nutY - spaceY * 3,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[4] = amountOfNutPerRound[4] + 9;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 9,
                                         -i * spaceY + nutY + spaceY * 5,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[4] = amountOfNutPerRound[4] + 12;
        //going down, starting right
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 27,
                                         -i * spaceY + nutY - spaceY * 4,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[4] = amountOfNutPerRound[4] + 8;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 10,
                                         -i * spaceY + nutY + spaceY * 5,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[4] = amountOfNutPerRound[4] + 12;
    }
    void Round5()
    {
        amountOfNutPerRound[5] = 0;
        //going down, starting right
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 28,
                                         -i * spaceY + nutY - spaceY * 4,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[5] = amountOfNutPerRound[5] + 8;
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 24; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 4,
                                         -i * spaceY + nutY - spaceY * 12,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[5] = amountOfNutPerRound[5] + 25;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 11,
                                         -i * spaceY + nutY + spaceY * 5,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[5] = amountOfNutPerRound[5] + 12;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            //          Debug.Log("in first for loop");
            for (int j = 0; j < xNutAmountSquare; j++)
            {
                //                Debug.Log("in second for loop");
                nutPos = new Vector3(j * spaceX - nutX - 11 * spaceX,
                                         -i * spaceY + nutY + 5 * spaceY,
                                         nutZ);
                newNut(nutPos);
                //   Debug.Log("yo");
            }
        }
        amountOfNutPerRound[5] = amountOfNutPerRound[5] + xNutAmountSquare;
        //going down, starting right
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 29,
                                         -i * spaceY + nutY - spaceY * 5,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[5] = amountOfNutPerRound[5] + 8;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 12,
                                         -i * spaceY + nutY + spaceY * 6,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[5] = amountOfNutPerRound[5] + 12;
    }
    void Round6()
    {
        amountOfNutPerRound[6] = 0;
        //going down, starting right
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 30,
                                         -i * spaceY + nutY - spaceY * 6,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[6] = amountOfNutPerRound[6] + 7;
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 25; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 5,
                                         -i * spaceY + nutY - spaceY * 13,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[6] = amountOfNutPerRound[6] + 26;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 13,
                                         -i * spaceY + nutY + spaceY * 6,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[6] = amountOfNutPerRound[6] + 12;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < xNutAmountSquare + 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - 13 * spaceX,
                                         -i * spaceY + nutY + 6 * spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[6] = amountOfNutPerRound[6] + xNutAmountSquare + 1;
        //going down, starting right
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 31,
                                         -i * spaceY + nutY - spaceY * 7,
                                         nutZ);
                newNut(nutPos);
                //Debug.Log("HI");
            }
        }
        amountOfNutPerRound[6] = amountOfNutPerRound[6] + 7;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 14,
                                         -i * spaceY + nutY + spaceY * 7,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[6] = amountOfNutPerRound[6] + 12;
    }

    void Round7()
    {

    }

    void newNut(Vector3 nutPos)
    {
        GameObject tempNut = Instantiate(nutScoreObject, nutPos + gameObject.transform.position,
                    Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z), gameObject.transform);
        nutPool.Add(tempNut);
        MeshRenderer[] meshrends = tempNut.GetComponentsInChildren<MeshRenderer>();
        //Added in later when "nut art" is done.
        for (int k = 0; k < meshrends.Length; k++)
        {
            meshrends[k].enabled = false;
        }
    }

    public void TurnNutOn(int nutType, int nutRound)
    {
        nutCount++;
        switch (nutRound)
        {
            case 0:
                compiledNutPerRound = amountOfNutPerRound[0];
                break;
            case 1:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1];
                break;
            case 2:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2];
                break;
            case 3:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3];
                break;
            case 4:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4];
                break;
            case 5:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4] + amountOfNutPerRound[5];
                break;
            case 6:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4] + amountOfNutPerRound[5] + amountOfNutPerRound[6];
                break;
        }
        //Debug.Log("nutCount: " + nutCount + " compiledNutPerRound: " + compiledNutPerRound);
        MeshRenderer[] meshrends = nutPool[nutCount - 1].GetComponentsInChildren<MeshRenderer>();
        meshrends[nutType].enabled = true;
        if(nutCount == compiledNutPerRound)
        {
            if (nutRound == 0)
            {
                // SceneManager.LoadScene(2); //For playtesting builds
                MoveEverythingBack(roundPositions[nutRound + 1], 0);
                currentNutRound = nutRound + 1;
            }
            else if (nutRound == 1)
            {
                MoveEverythingBack(roundPositions[nutRound + 1], 0);
                currentNutRound = nutRound + 1;
            }
            else if (nutRound == 2)
            {
                MoveEverythingBack(roundPositions[nutRound + 1], 0);
                currentNutRound = nutRound + 1;
            }
            else if (nutRound == 3)
            {
                MoveEverythingBack(roundPositions[nutRound + 1], 0);
                currentNutRound = nutRound + 1;
            }
            else if (nutRound == 4)
            {
                MoveEverythingBack(roundPositions[nutRound + 1], 0);
                currentNutRound = nutRound + 1;
            }
            else if (nutRound == 5)
            {
                MoveEverythingBack(roundPositions[nutRound + 1], 0);
                currentNutRound = nutRound + 1;
            }
            else if (nutRound == 6)
            {
                MoveEverythingBack(roundPositions[nutRound + 1], 0);
                currentNutRound = nutRound + 1;
            }
        }
    }

    public void MoveEverythingBack(Vector3 roundPosition, int j)
    {
        gameObject.GetComponent<Transform>().position = roundPosition;
        currentNutRound++;
    }
    
}
