using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutMovement : MonoBehaviour
{
    private GameObject thisNut;
    [SerializeField]
    private Vector3 nutVector; //dependent on Nut Type, edited on prefab.
    [SerializeField]
    private Vector3 nutLimit; //this is how far on the screen the nut can go before it despawns

    private NutSpawner nutSpawner;
    private Menu menu;

    private void Start()
    {
        thisNut = gameObject;
        nutSpawner = FindObjectOfType<NutSpawner>();
        menu = FindObjectOfType<Menu>();
    }

    private void FixedUpdate()
    {
        if(!menu.menuOn && !menu.menuIsMoving)
        {
            if (thisNut.transform.localPosition.x <= nutLimit.x)
            {
                //print("we are destroying");
                nutSpawner.DecreaseNutCount();
                Destroy(thisNut);

            }
            else
            {
                //print(thisNut.transform.localPosition);
                thisNut.transform.localPosition = thisNut.transform.localPosition + nutVector;
            }
        }
       
    }
}
