using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

//this script moves the hammer back and forth on the world x axis according to the hinge.
public class HammerMovement : MonoBehaviour
{
    private GameObject hingeObject;
    private Transform hingeTransform;
    private Vector3 movementVector;
    
    [Header("Adjustments")]
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float xMin;

    //this is the bool that is true if the hammer's x pos is increasing.
    private bool goingRight; 
    private void Start()
    {
        hingeObject = gameObject;
        hingeTransform = hingeObject.transform;
        
        float randomizer = UnityEngine.Random.Range(0f, 2f);
        if (randomizer > 1f)
            goingRight = true;
        else if (randomizer <= 1f)
            goingRight = false;
    }

    private void FixedUpdate()
    {
        if (goingRight)
        {
            if (hingeTransform.position.x >= xMax)
            {
                goingRight = !goingRight;
            }
            else if (hingeTransform.position.x < xMax)
            {
                movementVector = new Vector3(1,0,0);
            }
        }
        if (!goingRight)
        {
            if (hingeTransform.position.x <= xMin)
            {
                goingRight = !goingRight;
            }
            else if (hingeTransform.position.x > xMin)
            {
                movementVector = new Vector3(-1,0,0);
            }
        }
        hingeObject.transform.position = hingeObject.transform.position + movementVector;
    }
}
