using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStabilizer : MonoBehaviour
{
    public GameObject hammerHinge;
    public float xMod;
    private Transform hingeTransform;
    private Vector3 hammerPos;

    private void Start()
    {
        hingeTransform = hammerHinge.GetComponent<Transform>();

    }

    private void FixedUpdate()
    {
        hammerPos = new Vector3(hingeTransform.position.x + xMod, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = hammerPos;
    }
}
