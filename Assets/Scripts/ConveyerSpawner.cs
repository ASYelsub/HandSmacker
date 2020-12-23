using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerSpawner : MonoBehaviour
{
    [SerializeField]
    private Menu menu;
    [SerializeField]
    private GameObject conveyerPrefab;
    [SerializeField]
    private Vector3 initialLocation;
    [SerializeField] 
    private Vector3 initialRotation;
    [SerializeField] 
    private Vector3 secondInitialPos;

    public Vector3 despawnPos;
    
    private void Start()
    {
        Instantiate(conveyerPrefab, initialLocation,Quaternion.Euler(initialRotation));
        Instantiate(conveyerPrefab, secondInitialPos, Quaternion.Euler(initialRotation));
    }

    public void SpawnNextConveyer()
    {
        Instantiate(conveyerPrefab, secondInitialPos, Quaternion.Euler(initialRotation));
    }
}
