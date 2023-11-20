using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyVision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "PlayerBody") return;

        other.GetComponent<Movement>().Respawn();
    }
}
