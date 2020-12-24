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
    [SerializeField] private Menu menu;

    [SerializeField] private float hammerDownTimeLimit;
    [SerializeField] private float hammerUpTimeLimit;
    [SerializeField] private Vector3 hammerDownVelocity;
    [SerializeField] private Vector3 hammerUpVelocity;

    [SerializeField] private AudioClip[] hammerSound;
    private AudioSource hingeAudioSource;

    [SerializeField] private AudioClip nutCrunch;

    [SerializeField] private ParticleSystem hammerSpark;
    [SerializeField] private ParticleSystem firework;

    [SerializeField] private ParticleSystem[] fireworks;

    [SerializeField]
    private GameObject flowerHolder;

    private Color[] fireworkColors = new Color[5];

    public float fireworkTimer = 0;

    private bool audioPlaying;

    private void Start(){
        fireworkColors[0] = new Vector4(160,33,209,255);
        fireworkColors[1] = new Vector4(0,250,255,255);
        fireworkColors[2] = new Vector4(255,0,47,255);
        fireworkColors[3] = new Vector4(0, 26, 255, 255);
        fireworkColors[4] = new Vector4(195,195,88,255);
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
        if(!menu.menuOn && !menu.menuIsMoving)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                if (!hammerIsAttacking && !hammerIsGoingUp)
                {
                    hammerIsAttacking = true;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if(!menu.menuOn && !menu.menuIsMoving)
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
                        int randomInt = UnityEngine.Random.Range(0, hammerSound.Length);
                        hingeAudioSource.PlayOneShot(hammerSound[randomInt]);
                        hammerSpark.Emit(30);
                    }

                    //Am currently unsure how screen shake should manifest for non-nut hammering.
                    //Don't want to mentally exhaust the player by having them see the screen shake too much.
                    //Also the impact of the nut smash is lessened if there is screen shake all the time.
                    if (!nutCollision)
                    {
                        StartCoroutine(cameraShake.Shake(1f * cameraShake.duration, 1f * cameraShake.magnitude));
                    }
                    /*
                    if (!flowerGen.enumRunning) {
                        StartCoroutine(flowerGen.SpawnFlower(fireworkTimer));
                    }*/
                    StartCoroutine(SpawnFirework(fireworkTimer * 5));


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
        
    }
    private IEnumerator SpawnFirework(float fireworkTimer)
    {
        yield return new WaitForSeconds(.2f);
        int fireworkTimerInt = (int)fireworkTimer;

        
        
        int randomInt = UnityEngine.Random.Range(0, fireworks.Length);
        int randomColor = UnityEngine.Random.Range(0, fireworkColors.Length);

        var main = fireworks[randomInt].main;
        main.startColor = fireworkColors[randomColor];
        fireworks[randomInt].Emit(fireworkTimerInt);
        yield return null;
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
        scoreNutSpawner.TurnNutOn(tempint);
        Destroy(other.gameObject);
        print(other.gameObject);
        //scoreMovement.UpdateScore(score);
        /*if (!cameraShake.corIsRunning)
        {
            StartCoroutine(cameraShake.Shake(cameraShake.duration, cameraShake.magnitude));
        }*/
    }
}
