using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private PlayerVision visionArea;
    [SerializeField] private Movement playerMovement;

    public void Teleport(InputAction.CallbackContext context)
    {
        if (context.performed && visionArea.CanTeleport)
        {
            var newPos = visionArea.CurrentTeleport.transform.position;
            newPos.y += playerMovement.transform.localScale.y / 2;
            transform.position = newPos;

            visionArea.CanTeleport = false;
            visionArea.CurrentTeleport = null;

            playerMovement.rb.velocity = new Vector2(playerMovement.rb.velocity.x,0);
        }
    }
}
