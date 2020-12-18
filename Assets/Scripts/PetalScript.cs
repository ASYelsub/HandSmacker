using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script goes on the "flower petals" that are prefabs that are spawned in flower gen.
public class PetalScript : MonoBehaviour
{
    public void DeletePetal()
    {
        Destroy(gameObject);
    }
}
