using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    private bool canTeleport;

    public bool CanTeleport
    {
        get => canTeleport;
        set => canTeleport = value;
    }

    private TeleportSpot currentTeleport;
    public TeleportSpot CurrentTeleport
    {
        get => currentTeleport;
        set => currentTeleport = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy" && other.tag != "TeleportStone") return;

        canTeleport = true;
        currentTeleport = other.GetComponentInChildren<TeleportSpot>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Enemy" && other.tag != "TeleportStone") return;

        canTeleport = false;
        currentTeleport = null;
    }
}
