using System.Collections;
using System.Collections.Generic;
using DFA;
using UnityEngine;

public class Menu : MonoBehaviour
{
    Transform menuTransform;

    [SerializeField]
    private Transform enterTransform;

    [HideInInspector]public bool menuOn;
    [HideInInspector]public bool menuIsMoving;
    [SerializeField]Vector3 menuOnPos;
    [SerializeField]Vector3 menuOffPos;
    [SerializeField]Vector3 enterPos;
    [SerializeField]Vector3 enterHiddenPos;
    [Header ("Options")]
    [SerializeField] private GameObject hapticsToggle;
    [SerializeField] private GameObject screenshakeToggle;
    [SerializeField] private GameObject shaderToggle;
    [HideInInspector]public bool hapticsOn;
    [HideInInspector]public bool screenshakeOn;
    [HideInInspector] public bool shaderOn;


    [Header("Audio Stuff")]
    [SerializeField] private GameObject[] musicVol;
    [SerializeField] private GameObject[] sfxVol;
    [SerializeField] private Material selectMat;
    [SerializeField] private Material notSelectMat;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip switchClick;

    [Header("Raycast Stuff")]
    Vector2 mousePos;
    [SerializeField]private Camera cam;
    [SerializeField] private GameObject closeMenuButton;
    bool shootRay;
    public Ray buttonRay;
    Vector3 point;
    Vector3 worldPos;
    void Start()
    {
        screenshakeOn = true;
        hapticsOn = true;
        shootRay = false;
        shaderOn = true;
        //scale of 0.126693,0.126693,0.08172395
        menuOn = false;
        menuIsMoving = false;
        menuTransform = gameObject.GetComponent<Transform>();
        menuTransform.localPosition = menuOffPos;
    }

    public void TurnMenuOnOff(bool currentMenuState)
    {
        sfxSource.PlayOneShot(switchClick);
        if (!currentMenuState)
        {
            StartCoroutine(MoveMenuUp());
        }
        else if (currentMenuState)
        {
            StartCoroutine(MoveMenuDown());
        }
    }

    private void setMusicVolume(int i)
    {
        GameObject pickedVol = musicVol[i];
        sfxSource.PlayOneShot(click);
        for (int j = 0; j < musicVol.Length; j++)
        {
            musicVol[j].GetComponent<MeshRenderer>().material = notSelectMat;
        }
        switch (i)
        {
            case 0:
                musicSource.GetComponent<AudioSource>().volume = 0.05f;
                break;
            case 1:
                musicSource.GetComponent<AudioSource>().volume = 0.2f;
                break;
            case 2:
                musicSource.GetComponent<AudioSource>().volume = 0.3f;
                break;
            case 3:
                musicSource.GetComponent<AudioSource>().volume = 0.4f;
                break;
            case 4:
                musicSource.GetComponent<AudioSource>().volume = 0.5f;
                break;
            case 5:
                musicSource.GetComponent<AudioSource>().volume = 0.6f;
                break;
            case 6:
                musicSource.GetComponent<AudioSource>().volume = 0.7f;
                break;
            case 7:
                musicSource.GetComponent<AudioSource>().volume = 0.94f;
                break;
        }
        musicVol[i].GetComponent<MeshRenderer>().material = selectMat;
    }

    private void setSFXVolume(int i)
    {
        GameObject pickedVol = sfxVol[i];
        sfxSource.PlayOneShot(click);
        for(int j = 0; j < sfxVol.Length; j++)
        {
            sfxVol[j].GetComponent<MeshRenderer>().material = notSelectMat;
        }
        switch (i)
        {
            case 0:
                sfxSource.GetComponent<AudioSource>().volume = 0.05f;
                break;
            case 1:
                sfxSource.GetComponent<AudioSource>().volume = 0.2f;
                break;
            case 2:
                sfxSource.GetComponent<AudioSource>().volume = 0.3f;
                break;
            case 3:
                sfxSource.GetComponent<AudioSource>().volume = 0.4f;
                break;
            case 4:
                sfxSource.GetComponent<AudioSource>().volume = 0.5f;
                break;
            case 5:
                sfxSource.GetComponent<AudioSource>().volume = 0.6f;
                break;
            case 6:
                sfxSource.GetComponent<AudioSource>().volume = 0.7f;
                break;
            case 7:
                sfxSource.GetComponent<AudioSource>().volume = 0.94f;
                break;
        }
        sfxVol[i].GetComponent<MeshRenderer>().material = selectMat;
    }

    private IEnumerator MoveMenuUp()
    {
        float timer = 0;
        while (timer < 1)
        {
            enterTransform.localPosition = Vector3.Lerp(enterPos, enterHiddenPos, timer);
            menuTransform.localPosition = Vector3.Lerp(menuOffPos, menuOnPos, timer);
            timer = timer + .03f;
            //Debug.Log(timer);
            yield return null;
        }
        menuIsMoving = false;
        menuOn = true;
        yield return null;
    }

    private IEnumerator MoveMenuDown()
    {
        float timer = 0;
        while (timer < 1)
        {
            enterTransform.localPosition = Vector3.Lerp(enterHiddenPos, enterPos, timer);
            menuTransform.localPosition = Vector3.Lerp(menuOnPos, menuOffPos, timer);
            timer = timer + .03f;
            yield return null;
        }
        menuIsMoving = false;
        menuOn = false;
        yield return null;
    }

    void Update()
    {
        MouseInputs();
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuIsMoving)
        {
            TurnMenuOnOff(menuOn);
            menuIsMoving = true;
        }
        if (shootRay)
        {
            ButtonRay();
        }
    }

    void MouseInputs()
    {
        mousePos = (new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        point = mousePos;
        point.z = cam.nearClipPlane;
        worldPos = cam.ScreenToWorldPoint(point);

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (!menuIsMoving)
            {
                //Debug.Log("Mouse Clicked");
                shootRay = true;
            }
            
        }
    }

    void toggleShader()
    {
        sfxSource.PlayOneShot(click);
        if (cam.gameObject.GetComponent<PixelNostalgia>().enabled == true)
        {
            cam.gameObject.GetComponent<PixelNostalgia>().enabled = false;
            shaderToggle.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            cam.gameObject.GetComponent<PixelNostalgia>().enabled = true;
            shaderToggle.GetComponent<MeshRenderer>().enabled = true;
        }
        shaderOn = !shaderOn;
    }

    void toggleScreenShake()
    {
        sfxSource.PlayOneShot(click);
        if (screenshakeOn)
        {
            screenshakeToggle.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            screenshakeToggle.GetComponent<MeshRenderer>().enabled = true;
        }
        screenshakeOn = !screenshakeOn;
    }
    void toggleHaptics()
    {
        sfxSource.PlayOneShot(click);
        if (hapticsOn)
        {
            hapticsToggle.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            hapticsToggle.GetComponent<MeshRenderer>().enabled = true;
        }
        hapticsOn = !hapticsOn;
    }

    void ButtonRay()
    {
        Debug.Log(worldPos.x + " " + worldPos.y);
        buttonRay = new Ray(new Vector3(worldPos.x, worldPos.y, cam.transform.position.z), Vector3.forward);
        //Debug.Log("Ray sent out");
        RaycastHit hit;
        if (Physics.Raycast(buttonRay, out hit, 10f))
        {
            //Debug.DrawRay(buttonRay.origin, buttonRay.direction, Color.magenta);
            Debug.Log("Ray hit " + hit.collider.gameObject);
            if (hit.collider.tag == "Exit")
            {
                TurnMenuOnOff(menuOn);
                menuIsMoving = true;
            }
            if(hit.collider.tag == "Enter")
            {
                TurnMenuOnOff(menuOn);
                menuIsMoving = true;
            }
            if(hit.collider.tag == "SFX1")
            {
                setSFXVolume(0);
            }
            if (hit.collider.tag == "SFX2")
            {
                setSFXVolume(1);
            }
            if (hit.collider.tag == "SFX3")
            {
                setSFXVolume(2);
            }
            if (hit.collider.tag == "SFX4")
            {
                setSFXVolume(3);
            }
            if (hit.collider.tag == "SFX5")
            {
                setSFXVolume(4);
            }
            if (hit.collider.tag == "SFX6")
            {
                setSFXVolume(5);
            }
            if (hit.collider.tag == "SFX7")
            {
                setSFXVolume(6);
            }
            if(hit.collider.tag == "Music1")
            {
                setMusicVolume(0);
            }
            if (hit.collider.tag == "Music2")
            {
                setMusicVolume(1);
            }
            if (hit.collider.tag == "Music3")
            {
                setMusicVolume(2);
            }
            if (hit.collider.tag == "Music4")
            {
                setMusicVolume(3);
            }
            if (hit.collider.tag == "Music5")
            {
                setMusicVolume(4);
            }
            if (hit.collider.tag == "Music6")
            {
                setMusicVolume(5);
            }
            if (hit.collider.tag == "Music7")
            {
                setMusicVolume(6);
            }
            if(hit.collider.tag == "Haptics")
            {
                toggleHaptics();
            }
            if(hit.collider.tag == "Screenshake")
            {
                toggleScreenShake();
            }
            if(hit.collider.tag == "Shader")
            {
                toggleShader();
            }

        }
        shootRay = false;
    }
}
