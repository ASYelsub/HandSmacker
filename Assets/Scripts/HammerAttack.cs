using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script makes the hammer attack down when the player presses space
//attack down = rotating on the x axis one way and then another
public class HammerAttack : MonoBehaviour
{
    private GameObject hingeObject;
    private Transform hingeTransform;
    private Vector3 initialRotation; //must be under zero! do not go over zero! it will fuck up the math! fuck eulers!
    [SerializeField]
    private Vector3 hitRotation; //the min number that the hammer can be at before it goes back
    [SerializeField] 
    private Vector3 hitDownSpeed;

    private Vector3 movementVector;
    
    public bool hammerIsAttacking;
    

    private void Start()
    {
        hammerIsAttacking = false;
        hingeObject = gameObject;
        hingeTransform = hingeObject.transform;
        initialRotation = hingeObject.transform.eulerAngles;
    }

    private void Update()
    {
        if (!hammerIsAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // print("HI THIS FUCKIN HAPPENED");
                hammerIsAttacking = true;
            }   
        }
        else if (hammerIsAttacking)
        {
            //print("now we are true");
            if (hingeObject.transform.eulerAngles.x - 360f <= hitRotation.x) //watch the fuck out for euler angles, bro.
            {
                hammerIsAttacking = false;
                print("hammer finished attacking");
            }
            else
            {
                //print("in here???");
                movementVector = hitDownSpeed;
                hingeObject.transform.eulerAngles += movementVector;
            }
            //print(hingeObject.transform.eulerAngles.x - 360f); 
            
        }
    }
}
