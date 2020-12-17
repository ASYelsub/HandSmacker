using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGen : MonoBehaviour
{
    [SerializeField]
    private GameObject flowerPrefab;

    [SerializeField]
    private Material[] flowerMaterials;

    // Update is called once per frame

    public void GenFlower()
    {

    }

    IEnumerator WaitBetweenSpawn()
    {
        yield return null;
    }
}
