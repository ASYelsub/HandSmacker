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
    private bool nutCollision;

    public int score;
    private float timer = 0f;

    private int tempint;
    [Header("Other Scripts")]
    //[SerializeField] private ScoreMovement scoreMovement;
    [SerializeField] private NutSpawner nutSpawner;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private ScoreNutSpawner scoreNutSpawner;
    [SerializeField] private FlowerGen flowerGen;

    [SerializeField] private float hammerDownTimeLimit;
    [SerializeField] private float hammerUpTimeLimit;
    [SerializeField] private Vector3 hammerDownVelocity;
    [SerializeField] private Vector3 hammerUpVelocity;

    [SerializeField] private AudioClip[] hammerSound;
    private AudioSource hingeAudioSource;

    [SerializeField] private AudioClip nutCrunch;

    [SerializeField] private ParticleSystem hammerSpark;
    [SerializeField] private ParticleSystem firework;

    [SerializeField]
    private GameObject flowerHolder;

    public float fireworkTimer = 0;

    private bool audioPlaying;
    private void Start(){
        hammerIsAttacking = false;
        hammerIsGoingUp = false;
        audioPlaying = false;
        nutCollision = false;
        hingeObject = gameObject;
        hingeTransform = hingeObject.transform;
        hingeAudioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (!hammerIsAttacking && !hammerIsGoingUp)
            {
                hammerIsAttacking = true;   
            }
        }
    }

    private void FixedUpdate()
    {

        fireworkTimer += Time.deltaTime;
        
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
                if (!audioPlaying)
                {
                    int randomInt = UnityEngine.Random.Range(0,hammerSound.Length);
                    hingeAudioSource.PlayOneShot(hammerSound[randomInt]);
                    hammerSpark.Emit(30);
                }

                //Am currently unsure how screen shake should manifest for non-nut hammering.
                //Don't want to mentally exhaust the player by having them see the screen shake too much.
                //Also the impact of the nut smash is lessened if there is screen shake all the time.
                if(!nutCollision)
                {
                    StartCoroutine(cameraShake.Shake(1f * cameraShake.duration, 1f * cameraShake.magnitude));
                }

                StartCoroutine(flowerGen.SpawnFlower(fireworkTimer));
                //firework.Emit(1);
                //int fireworkTimerInt = (int) fireworkTimer;
                //firework.Emit(fireworkTimerInt);

                //scoreMovement.UpdateScoreMultiplier(fireworkTimerInt);
                fireworkTimer = 0;
                nutCollision = false;
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
                audioPlaying = false;
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        nutCollision = true;
        nutSpawner.DecreaseNutCount();
        audioPlaying = true;
        score++;
        //print(score);
        hingeAudioSource.PlayOneShot(nutCrunch);
        if (other.gameObject.CompareTag("almond"))
        {
            tempint = 0;
        }else if (other.gameObject.CompareTag("cashew"))
        {
            tempint = 1;
        }else if (other.gameObject.CompareTag("pecan"))
        {
            tempint = 2;
        }
        scoreNutSpawner.SpawnNut(tempint);
        Destroy(other.gameObject);
        print(other.gameObject);
        //scoreMovement.UpdateScore(score);
        /*if (!cameraShake.corIsRunning)
        {
            StartCoroutine(cameraShake.Shake(cameraShake.duration, cameraShake.magnitude));
        }*/
    }
}
