﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerMovement : MonoBehaviour
{
    private ConveyerSpawner conveyerSpawner;
    [SerializeField]
    private Vector3 conveyerMoveVector;
    void Start()
    {
        conveyerSpawner = FindObjectOfType<ConveyerSpawner>();
    }

    private void FixedUpdate()
    {
        
        if (this.transform.position.x <= conveyerSpawner.despawnPos.x)
        {
            conveyerSpawner.SpawnNextConveyer();
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.localPosition = gameObject.transform.localPosition + conveyerMoveVector;
        }
    }
}
