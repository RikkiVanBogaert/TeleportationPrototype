using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTeleportButton : MonoBehaviour
{
    [SerializeField] private GameObject button;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        button.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        button.SetActive(false);
    }
}
