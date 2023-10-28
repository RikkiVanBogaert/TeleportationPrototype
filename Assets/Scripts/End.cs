using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "PlayerBody") return;

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            //Reload same level
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Reload level 0
            SceneManager.LoadScene(0);
        }
    }
}
