using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGen : MonoBehaviour
{
    [SerializeField]
    private GameObject flowerPrefab;

    [Header("Tuning Variables")]
    [SerializeField]
    private float flowerExpand;  //around 1-5
    [SerializeField]
    private float spacer; //should be less than 1, i like somewhere between .3 and .4
    [SerializeField]
    private float genSpeed; //like .1
    [SerializeField]
    private float destructSpeed; //like .05?

    [SerializeField]
    private Material[] flowerMaterials;

    private List<GameObject> flowerPetals;

    private Vector3 petalPos;
    private Vector3 initialPos;
    private Vector3 petalRot = new Vector3(-90, 0, 0);
    private float petalX;
    private float petalY;
    private float petalZ;

    private Vector3[] savedPetalPos = new Vector3[3];



    bool yDecreased = false;
    bool yIncreased = false;
    bool xDecreased = false;
    bool xIncreased = false;

    private void Start()
    {
        flowerPetals = new List<GameObject>();
        initialPos = flowerPrefab.GetComponent<Transform>().position;
        
        petalX = 0f;
        petalY = 0f;
        petalZ = 0f;
    }

    public IEnumerator SpawnFlower(float amountOfTime)
    {

        /*Pseudocode for algorithm
         * FIRST FIVE PETALS
         * Spawn first petal in middle
         * pick one of four directions to go in
         * spawn second petal in that direction
         * save coordinates of second petal in new vector3
         * reset petalPos to 0
         * spawn third petal in one of three directions, subtracting where the second petal went
         * save coordinates of third petal
         * reset petal pos to 0
         * spawn fourth petal in one of two remaining directions
         * save coordinates of fourth petal
         * reset petal post to 0
         * spawn fifth petal in remaining direction
         */

        int spawnCount = (int)(amountOfTime * flowerExpand);

        

        for (int i = 0; i < 5; i++)
        {

            if (i != 0 && i < 5)
            {
                petalPos = firstFourAfter0Logic(i);
                //savedPetalPos[i - 1] = petalPos;
            }
            
            
            //technically i = 0 as well
            GameObject flowerPetal = Instantiate(flowerPrefab, initialPos + petalPos, Quaternion.Euler(petalRot));


            petalX = 0f;
            petalY = 0f;
            petalZ = 0f;
            //petalPos = new Vector3(petalX, petalY, petalZ);

            int flowerColor = UnityEngine.Random.Range(0, flowerMaterials.Length);
            flowerPetal.GetComponent<MeshRenderer>().material = flowerMaterials[flowerColor];
            yield return new WaitForSeconds(genSpeed);
            flowerPetals.Add(flowerPetal);
            
        }

        //Is this what memory management is????
        for(int i = flowerPetals.Count - 1; i >= 0; i--)
        {
            flowerPetals[i].GetComponent<PetalScript>().DeletePetal();
            yield return new WaitForSeconds(destructSpeed);
        }
        flowerPetals.Clear();
       
        yield return null;
    }

    


    private bool decreaseX(bool stateofBool)
    {
        if (stateofBool == false)
        {
            petalX = petalX - spacer;
        }
        xIncreased = true;
        return true;
    }
    private bool decreaseY(bool stateofBool)
    {
        if (stateofBool == false)
        {
            petalY = petalY - spacer;
        }
        yDecreased = true;
        return true;
    }
    private bool increaseX(bool stateofBool)
    {
        if (stateofBool == false)
        {
            petalX = petalX + spacer;
        }
        xDecreased = true;
        return true;
    }
    private bool increaseY(bool stateofBool)
    {
        if (stateofBool == false)
        {
            petalY = petalY + spacer;
        }
        yIncreased = true;
        return true;
    }


    private Vector3 firstFourAfter0Logic(int currentNum)
    {
        if (currentNum == 1)
        {
            int randomMove = UnityEngine.Random.Range(0, 4);
            switch (randomMove)
            {
                case 0:
                    increaseX(false);
                    break;
                case 1:
                    increaseY(false);
                    break;
                case 2:
                    decreaseX(false);
                    break;
                case 3:
                    decreaseY(false);
                    break;
            }
            petalPos = new Vector3(petalX, petalY, petalZ);
        }
        else if (currentNum == 2)
        {
            int randomMove = UnityEngine.Random.Range(0, 3);
            if (xIncreased)
            {
                switch (randomMove)
                {
                    case 0: increaseY(false); break;
                    case 1: decreaseX(false); break;
                    case 2: decreaseY(false); break;
                }
            }
            else if (yIncreased)
            {
                switch (randomMove)
                {
                    case 0: increaseX(false); break;
                    case 1: decreaseX(false); break;
                    case 2: decreaseY(false); break;
                }
            }
            else if (xDecreased)
            {
                switch (randomMove)
                {
                    case 0: increaseY(false); break;
                    case 1: increaseX(false); break;
                    case 2: decreaseY(false); break;
                }
            }
            else if (yDecreased)
            {
                switch (randomMove)
                {
                    case 0: decreaseX(false); break;
                    case 1: increaseX(false); break;
                    case 2: increaseY(false); break;
                }
            }

            // Debug.Log(petalX + "," + petalY);
            petalPos = new Vector3(petalX, petalY, petalZ);
        }

        else if (currentNum == 3)
        {
            int randomMove = UnityEngine.Random.Range(0, 2);
            if (xDecreased && yDecreased)
            {
                switch (randomMove)
                {
                    case 0: increaseX(false); break;
                    case 1: increaseY(false); break;
                }
            }
            else if (xDecreased && xIncreased)
            {
                switch (randomMove)
                {
                    case 0: increaseY(false); break;
                    case 1: decreaseY(false); break;
                }
            }
            else if (xDecreased && yIncreased)
            {
                switch (randomMove)
                {
                    case 0: increaseX(false); break;
                    case 1: decreaseY(false); break;
                }
            }
            else if (yDecreased && xIncreased)
            {
                switch (randomMove)
                {
                    case 0: increaseY(false); break;
                    case 1: decreaseX(false); break;
                }
            }
            else if (yDecreased && yIncreased)
            {
                switch (randomMove)
                {
                    case 0: increaseY(false); break;
                    case 1: decreaseX(false); break;
                }
            }
            else if (xIncreased && yIncreased)
            {
                switch (randomMove)
                {
                    case 0: decreaseX(false); break;
                    case 1: decreaseY(false); break;
                }
            }
            petalPos = new Vector3(petalX, petalY, petalZ);
        }
        else if (currentNum == 4)
        {
            Debug.Log("hi");
        }
        Debug.Log(petalPos.ToString());
        return petalPos;
    }


}
