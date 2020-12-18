using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGen : MonoBehaviour
{
    [SerializeField]
    private GameObject flowerPrefab;

    [SerializeField]
    private Material[] flowerMaterials;


    public void GenFlowers(float amountOfTime)
    {

    }

    IEnumerator WaitBetweenSpawn()
    {
        yield return null;
    }
}
