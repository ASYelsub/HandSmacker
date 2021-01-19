using System.Collections;
using System.Collections.Generic;
using DFA;
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
    [SerializeField] private ScoreNutSpawner scoreNutSpawner16x9;
    [SerializeField] private ScoreNutSpawner scoreNutSpawner4x3;
    [SerializeField] private ScoreNutSpawner scoreNutSpawner18x9;

    private float screenWidth;
    private float screenHeight;

    private void Awake()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        if (screenWidth / screenHeight <= 1.65f)
        {
            //4x3
            fourByThreeSS.SetActive(true);
            sixteenByNineSS.SetActive(false);
            eighteenByNineSS.SetActive(false);
            StartCoroutine(RefreshCam(0));


        }
        if (screenWidth / screenHeight >= 2.0f)
        {
            //18x9
            fourByThreeSS.SetActive(false);
            sixteenByNineSS.SetActive(false);
            eighteenByNineSS.SetActive(true);
            StartCoroutine(RefreshCam(1));
        }
        if (screenWidth / screenHeight < 2.0f && screenWidth / screenHeight > 1.65f)
        {
            //16x9
            fourByThreeSS.SetActive(false);
            sixteenByNineSS.SetActive(true);
            eighteenByNineSS.SetActive(false);
            StartCoroutine(RefreshCam(2));
        }
    }

    public IEnumerator RefreshCam(int camType)
    {
        
        yield return new WaitForSecondsRealtime(.01f);
        if(StartScript.currentScene == 0)
        {
            switch (camType)
            {
                case 0:
                    cam4x3SS.gameObject.GetComponent<PixelNostalgia>().enabled = false;
                    yield return new WaitForSecondsRealtime(.01f);
                    cam4x3SS.gameObject.GetComponent<PixelNostalgia>().enabled = true;
                    break;
                case 1:
                    cam18x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = false;
                    yield return new WaitForSecondsRealtime(.01f);
                    cam18x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = true;
                    break;
                case 2:
                    cam16x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = false;
                    yield return new WaitForSecondsRealtime(.01f);
                    cam16x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = true;
                    Debug.Log("HELLO");
                    break;
            }
        }
        else{
            switch (camType)
            {
                case 0:
                    cam4x3SS.gameObject.GetComponent<PixelNostalgia>().enabled = false;
                    yield return new WaitForSecondsRealtime(.01f);
                    cam4x3SS.gameObject.GetComponent<PixelNostalgia>().enabled = true;
                   //scoreNutSpawner4x3.
                    break;
                case 1:
                    cam18x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = false;
                    yield return new WaitForSecondsRealtime(.01f);
                    cam18x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = true;
                    //scoreNutSpawner18x9.NutPool();
                    break;
                case 2:
                    cam16x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = false;
                    yield return new WaitForSecondsRealtime(.01f);
                    cam16x9SS.gameObject.GetComponent<PixelNostalgia>().enabled = true;
                    //scoreNutSpawner16x9.NutPool();
                    Debug.Log("HELLO");
                    break;
            }
        }
        
        yield return null;
    }

}
