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
    [SerializeField] private float xNutAmountSquare;
    [SerializeField] private float yNutAmountSquare;
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
        roundPositions[0] = new Vector3(gameObject.GetComponent<Transform>().position.x,gameObject.GetComponent<Transform>().position.y,gameObject.GetComponent<Transform>().position.z);
        roundPositions[1] = new Vector3(0.03f,0.4f,-0.4f);
        roundPositions[2] = new Vector3(0.03f,0.59f,1.34f);
        roundPositions[3] = new Vector3(0.03f,0.88f,3f);
        roundPositions[4] = new Vector3(0.03f,1.16f,5.4f);
        roundPositions[5] = new Vector3(0.08f,1.34f,7.19f);
        roundPositions[6] = new Vector3(0.08f,1.61f,8.76f);
        roundPositions[7] = new Vector3(-0.15f,1.82f,10.6f);
        roundPositions[8] = new Vector3(-0.62f,2.2f,12.69f);
        roundPositions[9] = new Vector3(-0.62f,2.38f,14.49f);
        roundPositions[10] = new Vector3(-0.62f,2.83f,16.46f);
        roundPositions[11] = new Vector3(1.2f,3.44f,18.08f);
        roundPositions[12] = new Vector3(1.73f,4.18f,21.32f);

        nutZ = 0f;

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


        //Round 0
        for (int i = 0; i < yNutAmountSquare; i++)
        {
            for (int j = 0; j < xNutAmountSquare; j++)
            {
                SpawnNut(i, j);
            }
        }
        //Round 1
        //Round 2
        //Round 3
        //Round 4
        //Round 5
        //Round 6
        //Round 7
        //Round 8
        //Round 9
        //Round 10
        //Round 11
        //Round 12
    }

    
    void SpawnNut(int i, int j)
    {
        nutPos = new Vector3(j * spaceX - nutX,
                                         -i * spaceY + nutY,
                                         nutZ);
        GameObject tempNut = Instantiate(nutScoreObject, nutPos + gameObject.transform.position,
            Quaternion.Euler(gameObject.transform.rotation.x,gameObject.transform.rotation.y,gameObject.transform.rotation.z), gameObject.transform);
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
        if(nutCount == xNutAmountSquare * yNutAmountSquare)
        {
            MoveEverythingBack(0);
        }
    
    }

    public void MoveEverythingBack(int j)
    {
        
        if (j == 0)
        {
            Vector3 addVec = new Vector3(-0.02f, 0.08f, 0.89f) - gameObject.GetComponent<Transform>().position;
            gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + addVec;
        }
    


    }
    
}
