using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

//This script moves the hammer back and forth at random speeds on the world x axis according to the hinge.
//This script stops movement momentarily when the bool hammerIsAttacking is true on HammerAttack.
public class HammerMovement : MonoBehaviour
{
    private GameObject hingeObject;
    private Transform hingeTransform;
    private Vector3 movementVector;
    [Header("Other Scripts")]
    [SerializeField]
    private HammerAttack hammerAttack;
    [SerializeField]
    private Menu menu;
    [Header("Adjustments")]
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float xMin;
    //These two should probably be around .01 to .3
    [SerializeField] private float movementSpeedMinimum;
    [SerializeField] private float movementSpeedMaximum;

    private float movementSpeed;

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
        RandomizeMovementSpeed();
    }

    private void FixedUpdate()
    {
        if(!menu.menuOn && !menu.menuIsMoving)
        {
            if (goingRight)
            {
                if (hingeTransform.position.x >= xMax)
                {
                    goingRight = !goingRight;
                    RandomizeMovementSpeed();
                }
                else if (hingeTransform.position.x < xMax)
                {
                    movementVector = new Vector3(movementSpeed, 0, 0);
                }
            }
            if (!goingRight)
            {
                if (hingeTransform.position.x <= xMin)
                {
                    RandomizeMovementSpeed();
                    goingRight = !goingRight;
                }
                else if (hingeTransform.position.x > xMin)
                {
                    movementVector = new Vector3(-movementSpeed, 0, 0);
                }
            }
            hingeObject.transform.position = hingeObject.transform.position + movementVector;
        }
       
    }

    private void RandomizeMovementSpeed()
    {
        movementSpeed = UnityEngine.Random.Range(movementSpeedMinimum, movementSpeedMaximum);
    }
}
