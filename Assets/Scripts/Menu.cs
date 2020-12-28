using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    Transform menuTransform;

    [SerializeField]
    private Transform enterTransform;

    [HideInInspector]public bool menuOn;
    [HideInInspector]public bool menuIsMoving;
    Vector3 menuOnPos;
    Vector3 menuOffPos;
    Vector3 enterPos;
    Vector3 enterHiddenPos;
     
    //Raycast stuff
    Vector2 mousePos;
    [SerializeField]private Camera cam;
    [SerializeField] private GameObject closeMenuButton;
    bool shootRay;
    public Ray buttonRay;
    Vector3 point;
    Vector3 worldPos;
    void Start()
    {
        enterPos = new Vector3(0.6376439f, -0.3511f, 0.5639391f);
        enterHiddenPos = new Vector3(0.6809f,-0.3511f, 0.5639391f);
        shootRay = false;
        menuOnPos = new Vector3(0, -0.06700065f, 0.60448f);
        //scale of 0.126693,0.126693,0.08172395
        menuOffPos = new Vector3(0f, -0.754f, 0.60448f);
        menuOn = false;
        menuIsMoving = false;
        menuTransform = gameObject.GetComponent<Transform>();
        menuTransform.localPosition = menuOffPos;
    }

    public void TurnMenuOnOff(bool currentMenuState)
    {
        if (!currentMenuState)
        {
            StartCoroutine(MoveMenuUp());
        }
        else if (currentMenuState)
        {
            StartCoroutine(MoveMenuDown());

        }
    }


    private IEnumerator MoveMenuUp()
    {
        float timer = 0;
        while (timer < 1)
        {
            enterTransform.localPosition = Vector3.Lerp(enterPos, enterHiddenPos, timer);
            menuTransform.localPosition = Vector3.Lerp(menuOffPos, menuOnPos, timer);
            timer = timer + .01f;
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
            timer = timer + .01f;
            yield return null;
        }
        menuIsMoving = false;
        menuOn = false;
        yield return null;
    }

    void Update()
    {
        MouseInputs();
        if (Input.GetKeyDown(KeyCode.Escape) && !menuIsMoving)
        {
            TurnMenuOnOff(menuOn);
            menuIsMoving = true;
        }
        if(shootRay)
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
        }
        shootRay = false;
    }
}
