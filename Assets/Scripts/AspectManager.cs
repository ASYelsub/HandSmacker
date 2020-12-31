using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rowan said the three aspect ratios to care about are: 4x3, 16,9, 18x9


public class AspectManager : MonoBehaviour
{
    [Header("StartScreen")]
    [SerializeField] private GameObject sixteenByNineSS;
    [SerializeField] private GameObject fourByThreeSS;
    [SerializeField] private GameObject eighteenByNineSS;
    [SerializeField] private Camera cam16x9SS;
    [SerializeField] private Camera cam4x3SS;
    [SerializeField] private Camera cam18x9SS;

    private float screenWidth;
    private float screenHeight;

    private void Awake()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    private void Start()
    {
        if(screenWidth/screenHeight <= 1.65f)
        {
            //4x3
            fourByThreeSS.SetActive(true);
            sixteenByNineSS.SetActive(false);
            eighteenByNineSS.SetActive(false);
        }else if(screenWidth/screenHeight >= 2.0f)
        {
            //18x9
            fourByThreeSS.SetActive(false);
            sixteenByNineSS.SetActive(false);
            eighteenByNineSS.SetActive(true);
        }
        else
        {
            //16x9
            fourByThreeSS.SetActive(false);
            sixteenByNineSS.SetActive(true);
            eighteenByNineSS.SetActive(false);

        }
    }

}
