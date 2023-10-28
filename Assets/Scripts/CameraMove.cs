using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform startPos, endPos;
    [SerializeField] private Transform player;
    //private Camera camera;

    void Start()
    {
        //camera = GetComponent<Camera>();

        startPos.parent = null;
        endPos.parent = null;
    }

    void Update()
    {
        Vector3 newPos = startPos.position;

        if(player.position.x > startPos.position.x) newPos.x = player.position.x;
        if(player.position.x > endPos.position.x) newPos.x = endPos.position.x;


        if (player.position.y > startPos.position.y) newPos.y = player.position.y;
        if (player.position.y > endPos.position.y) newPos.y = endPos.position.y;

        transform.position = newPos;
    }
}
