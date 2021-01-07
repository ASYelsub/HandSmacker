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
    private Vector3[] roundPositions2 = new Vector3[13];
    private int amountAddedToSides;
    private int amountAddedToTopAndBottom;

    [HideInInspector]public int currentNutRound;
    [HideInInspector]public int[] amountOfNutPerRound; //hook this variable up
    [HideInInspector] public bool nutsMovingBack;

    [SerializeField]
    private GameObject peanutGuide;
    [SerializeField]
    private GameObject[] nutPrefabs;

    [SerializeField] 
    private GameObject[] insertedNutTypes;


    [SerializeField]private Vector3 initialPos;
    [SerializeField]private Vector3 initialRot;


    private int nutOverlayCycle;
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
        nutOverlayCycle = 0;
        nutsMovingBack = false;
        compiledNutPerRound = 0;
        amountOfNutPerRound = new int[13];
        roundPositions[0] = new Vector3(gameObject.GetComponent<Transform>().position.x,gameObject.GetComponent<Transform>().position.y,gameObject.GetComponent<Transform>().position.z);
        roundPositions2[0] = new Vector3(-0.3f, 0.4f, -2.3f);
        roundPositions[1] = new Vector3(0.03f,0.4f,-0.3f);
        roundPositions2[1] = new Vector3(0.03f,0.5f,-0.4f);
        roundPositions[2] = new Vector3(0.03f,0.59f,1.34f);
        roundPositions2[2] = new Vector3(0.03f,0.59f,1.34f);
        roundPositions[3] = new Vector3(0.03f,0.88f,3f);
        roundPositions2[3] = new Vector3(0.03f,0.88f,3f);
        roundPositions[4] = new Vector3(0.03f,1.16f,5.4f);
        roundPositions2[4] = new Vector3(0.03f,1.16f,5.4f);
        roundPositions[5] = new Vector3(0.08f,1.34f,7.19f);
        roundPositions2[5] = new Vector3(0.08f,1.34f,7.19f);
        roundPositions[6] = new Vector3(0.08f,1.7f,8.76f);
        roundPositions2[6] = new Vector3(0.08f,1.7f,8.76f);
        roundPositions[7] = new Vector3(-1f,2f,11.5f);
        roundPositions2[7] = new Vector3(-1f,2f,11.5f);
        roundPositions[8] = new Vector3(-0.8f,2.5f,13f);
        roundPositions2[8] = new Vector3(-0.8f,2.5f,13f);
        roundPositions[9] = new Vector3(-1f,2.8f,15f);
        roundPositions2[9] = new Vector3(-1f,2.8f,15f);
        roundPositions[10] = new Vector3(-0.62f,3f,17f);
        roundPositions2[10] = new Vector3(-0.62f,3f,17f);
        roundPositions[11] = new Vector3(1.2f,3.44f,18.08f);
        roundPositions2[11] = new Vector3(1.2f,3.44f,18.08f);
        roundPositions[12] = new Vector3(1.73f,4.18f,21.32f);
        roundPositions2[12] = new Vector3(1.73f,4.18f,21.32f);

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
        for (int i = 0; i < amountOfNutPerRound.Length; i++)
        {
            amountOfNutPerRound[i] = 0;
        }
        yNutAmountSquare = initialYNutAmount;
        xNutAmountSquare = initialXNutAmount;
        nutOverlayCycle = 0;
        Round0();
        nutOverlayCycle = 1;
        Round0();
        nutOverlayCycle = 0;
        Round1();
        nutOverlayCycle = 1;
        Round1();
        nutOverlayCycle = 0;
        Round2();
        nutOverlayCycle = 1;
        Round2();
        nutOverlayCycle = 0;
        Round3();
        nutOverlayCycle = 1;
        Round3();
        nutOverlayCycle = 0;
        Round4();
        nutOverlayCycle = 1;
        Round4();
        nutOverlayCycle = 0;
        Round5();
        nutOverlayCycle = 1;
        Round5();
        nutOverlayCycle = 0;
        Round6();
        nutOverlayCycle = 1;
        Round6();
        nutOverlayCycle = 0;
        Round7();
        nutOverlayCycle = 1;
        Round7();
        nutOverlayCycle = 0;
        Round8();
        nutOverlayCycle = 1;
        Round8();
        nutOverlayCycle = 0;
        Round9();
        nutOverlayCycle = 1;
        Round9();
        nutOverlayCycle = 0;
        Round10();
        nutOverlayCycle = 1;
        Round10();
        nutOverlayCycle = 0;
        Round11();
        nutOverlayCycle = 1;
        Round11();
    }
    private void Update()
    {
        if (nutCount < nutPool.Count && !nutsMovingBack)
        {
            if (Input.GetKey(KeyCode.L))
            {
                TurnNutOn(0, currentNutRound);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                TurnNutOn(0, currentNutRound);
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
        //going down, starting right
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 32,
                                         -i * spaceY + nutY - spaceY * 9,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[7] = amountOfNutPerRound[7] + 5;
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 25; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 6,
                                         -i * spaceY + nutY - spaceY * 14,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[7] = amountOfNutPerRound[7] + 26;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 15,
                                         -i * spaceY + nutY + spaceY * 7,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[7] = amountOfNutPerRound[7] + 12;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < xNutAmountSquare + 2; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - 15 * spaceX,
                                         -i * spaceY + nutY + 7 * spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[7] = amountOfNutPerRound[7] + xNutAmountSquare + 2;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 16,
                                         -i * spaceY + nutY + spaceY * 8,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[7] = amountOfNutPerRound[7] + 12;
    }
    void Round8()
    {
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 23; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 8,
                                         -i * spaceY + nutY - spaceY * 15,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[8] = amountOfNutPerRound[8] + 24;
        //going up, starting at left
        for (int i = 12; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 17,
                                         -i * spaceY + nutY + spaceY * 8,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[8] = amountOfNutPerRound[8] + 12;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < xNutAmountSquare; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - 15 * spaceX,
                                         -i * spaceY + nutY + 8 * spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[8] = amountOfNutPerRound[8] + xNutAmountSquare;
        //going up, starting at left
        for (int i = 10; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 18,
                                         -i * spaceY + nutY + spaceY * 7,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[8] = amountOfNutPerRound[8] + 10;
    }
    void Round9()
    {
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 20; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 10,
                                         -i * spaceY + nutY - spaceY * 16,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[9] = amountOfNutPerRound[9] + 21;
        //going up, starting at left
        for (int i = 8; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 19,
                                         -i * spaceY + nutY + spaceY * 6,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[9] = amountOfNutPerRound[9] + 8;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < xNutAmountSquare - 4; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - 12 * spaceX,
                                         -i * spaceY + nutY + 9 * spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[9] = amountOfNutPerRound[9] + xNutAmountSquare - 4;
        //going up, starting at left
        for (int i = 6; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 20,
                                         -i * spaceY + nutY + spaceY * 5,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[9] = amountOfNutPerRound[9] + 6;
    }
    void Round10()
    {
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 16; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 12,
                                         -i * spaceY + nutY - spaceY * 17,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[10] = amountOfNutPerRound[10] + 17;
        //going up, starting at left
        for (int i = 4; i > 0; i--)
        {
            for (int j = 0; j < 1; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - spaceX * 21,
                                         -i * spaceY + nutY + spaceY * 4,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[10] = amountOfNutPerRound[10] + 4;
        //going right, starting at top
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < xNutAmountSquare - 11; j++)
            {
                nutPos = new Vector3(j * spaceX - nutX - 7 * spaceX,
                                         -i * spaceY + nutY + 10 * spaceY,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[10] = amountOfNutPerRound[10] + xNutAmountSquare - 11;
    }
    void Round11()
    {
        //going left, starting bottom
        for (int i = 0; i < 1; i++)
        {
            for (int j = 8; j > -1; j--)
            {
                nutPos = new Vector3(j * spaceX - nutX + spaceX * 16,
                                         -i * spaceY + nutY - spaceY * 18,
                                         nutZ);
                newNut(nutPos);
            }
        }
        amountOfNutPerRound[11] = amountOfNutPerRound[11] + 9;
    }

    void newNut(Vector3 nutPos)
    {
        Vector3 addVec = new Vector3();
        if(nutOverlayCycle == 0)
        {
            addVec = new Vector3(0, 0, 0);
        }
        else if(nutOverlayCycle == 1)
        {
            addVec = new Vector3(spaceX / 2, -spaceY / 2, -.1f);
        }
        GameObject tempNut = Instantiate(nutScoreObject, nutPos + gameObject.transform.position + addVec,
                   Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z), gameObject.transform);
        nutPool.Add(tempNut);
        MeshRenderer[] meshrends = tempNut.GetComponentsInChildren<MeshRenderer>();
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
            case 7:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4] + amountOfNutPerRound[5] + amountOfNutPerRound[6] + amountOfNutPerRound[7];
                break;
            case 8:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4] + amountOfNutPerRound[5] + amountOfNutPerRound[6] + amountOfNutPerRound[7] + amountOfNutPerRound[8];
                break;
            case 9:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4] + amountOfNutPerRound[5] + amountOfNutPerRound[6] + amountOfNutPerRound[7] + amountOfNutPerRound[8] + amountOfNutPerRound[9];
                break;
            case 10:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4] + amountOfNutPerRound[5] + amountOfNutPerRound[6] + amountOfNutPerRound[7] + amountOfNutPerRound[8] + amountOfNutPerRound[9] + amountOfNutPerRound[10];
                break;
            case 11:
                compiledNutPerRound = amountOfNutPerRound[0] + amountOfNutPerRound[1] + amountOfNutPerRound[2] + amountOfNutPerRound[3] + amountOfNutPerRound[4] + amountOfNutPerRound[5] + amountOfNutPerRound[6] + amountOfNutPerRound[7] + amountOfNutPerRound[8] + amountOfNutPerRound[9] + amountOfNutPerRound[10] + amountOfNutPerRound[11];
                break;
        }
        Debug.Log("nutCount: " + nutCount + " compiledNutPerRound: " + compiledNutPerRound);
        MeshRenderer[] meshrends = nutPool[nutCount - 1].GetComponentsInChildren<MeshRenderer>();
        meshrends[nutType].enabled = true;
        if(nutCount/2 == compiledNutPerRound)
        {
            StartCoroutine(MoveOut(roundPositions[nutRound + 1], roundPositions2[nutRound]));
            if(currentNutRound == 11)
            {
                StartCoroutine(TurnPeanutOn());
            }
            currentNutRound = nutRound + 1;
        }else if(nutCount == compiledNutPerRound)
        {
            StartCoroutine(MoveOut(roundPositions2[nutRound], roundPositions[nutRound]));
            Debug.Log("Hello");
        }
        
    }

    public IEnumerator TurnPeanutOn()
    {
        yield return new WaitForSeconds(1f);
        float timer2 = 0;
        Color c = new Color(255,255,255,0);
        float alpha;
        while (timer2 < 1)
        {
            peanutGuide.GetComponent<SpriteRenderer>().color = c;
            alpha = Mathf.Lerp(0, 1, timer2);
            c.a = alpha;
            timer2 = timer2 + .001f;
            yield return null;
        }

        /*for (int i = 0; i < nutPool.Count; i++)
        {
            GameObject nut = nutPool[i];
            MeshRenderer[] meshrends = nut.GetComponentsInChildren<MeshRenderer>();
            for (int k = 0; k < meshrends.Length; k++)
            {
                meshrends[k].enabled = false;
            }
        } */       
        yield return null;
    }
    public IEnumerator MoveOut(Vector3 newRoundPos, Vector3 oldRoundPos)
    {
        
        nutsMovingBack = true;
        float timer = 0;
        
        while (timer < 1)
        {
            gameObject.GetComponent<Transform>().position = Vector3.Lerp(oldRoundPos, newRoundPos, timer);
            timer = timer + .01f;
            
            yield return null;
        }
        nutsMovingBack = false;
        yield return null;
    }
    
}
