using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I got this script from:
//https://www.gamasutra.com/blogs/VivekTank/20180709/321571/Different_Ways_Of_Shaking_Camera_In_Unity.php
public class CameraShake : MonoBehaviour
{
    [Header("Properties")]
    public float duration;
    public float magnitude;
    [SerializeField] private float screenShakeSize;
    [HideInInspector] public bool corIsRunning = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Shake(duration, magnitude));
        }
    }
    
    public IEnumerator Shake(float duration, float magnitude)
    {
        corIsRunning = true;
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(-screenShakeSize, screenShakeSize) * magnitude;
            float y = Random.Range(-screenShakeSize, screenShakeSize) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
        corIsRunning = false;
    }
}
