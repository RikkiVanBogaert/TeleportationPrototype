using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    private bool canTeleport;
    private Transform ogTransform;

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

    void Start()
    {
        ogTransform = transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && other.tag == "TeleportStone")
        {
            canTeleport = true;
            currentTeleport = other.GetComponentInChildren<TeleportSpot>();
        }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Ground") return;

            transform.localScale = new Vector2(Vector2.Distance(transform.position, other.transform.position),
            transform.localScale.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy" && other.tag == "TeleportStone")
        {
            canTeleport = false;
            currentTeleport = null;
        }
        else if (other.tag == "Ground")
        {
            transform.localScale = ogTransform.localScale;
        }
    }
}
