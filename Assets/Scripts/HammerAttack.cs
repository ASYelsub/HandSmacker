﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script makes the hammer attack down when the player presses space
//attack down = rotating on the x axis one way and then another
public class HammerAttack : MonoBehaviour
{
    private GameObject hingeObject;
    private Transform hingeTransform;
    private Vector3 movementVector;
    
    public bool hammerIsAttacking;
    public bool hammerIsGoingUp;

    private float timer = 0f;

    [SerializeField] private float hammerDownTimeLimit;
    [SerializeField] private float hammerUpTimeLimit;
    [SerializeField] private Vector3 hammerDownVelocity;
    [SerializeField] private Vector3 hammerUpVelocity;

    [SerializeField] private AudioClip[] hammerSound;
    private AudioSource hingeAudioSource;

    [SerializeField] private ParticleSystem firework;

    private void Start(){
        hammerIsAttacking = false;
        hammerIsGoingUp = false;
        hingeObject = gameObject;
        hingeTransform = hingeObject.transform;
        hingeAudioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hammerIsAttacking && !hammerIsGoingUp)
            {
                hammerIsAttacking = true;   
            }
        }
    }

    private void FixedUpdate(){
        

        if (hammerIsAttacking)
        {
            if (timer < hammerDownTimeLimit)
            {
                timer += Time.deltaTime;
                hingeTransform.Rotate(hammerDownVelocity);
            }
            else
            {
                hammerIsAttacking = false;
                timer = 0;
                hammerIsGoingUp = true;
                int randomInt = UnityEngine.Random.Range(0,hammerSound.Length);
                hingeAudioSource.PlayOneShot(hammerSound[randomInt]);
                firework.Emit(20);
            }
        }
        if (hammerIsGoingUp)
        {
            if (timer < hammerUpTimeLimit)
            {
                timer += Time.deltaTime;
                hingeTransform.Rotate(hammerUpVelocity);
            }
            else
            {
                hammerIsAttacking = false;
                timer = 0;
                hammerIsGoingUp = false;
            }
        }
        
    }
}
