using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Menu menu;
    private float timer = 0;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            timer+= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= 4f)
        {
            SceneManager.LoadScene(2);
        }
    }
}
