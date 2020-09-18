using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script spawns nuts, adds them to a list, and then moves all the transforms
//of the nuts in the list along the x axis until they reach a maximum position
//and then that nut is taken out of the list and all the other nuts are
//moved forward in the list.
public class NutSpawner : MonoBehaviour
{
    [Header("Nut Prefabs")]
    [SerializeField]
    private GameObject almond, cashew, pecan;

    private GameObject[] activeNuts;
    
    //plug this into activeNuts
    [SerializeField]
    private int maxAmountOfNutsOnScreen;




}
