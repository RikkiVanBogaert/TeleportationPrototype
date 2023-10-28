using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgainstWall : MonoBehaviour
{
    private bool isAgainstWall = false;

    public bool IsAgainstWall
    {
        get => isAgainstWall;
        //set => isAgainstWall = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag != "PlayerBody") return;

        isAgainstWall = true;

        //Debug.Log("Against Wall");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isAgainstWall = false;
    }
}
