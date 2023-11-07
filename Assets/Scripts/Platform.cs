using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new Vector2(0,0);

    public Vector2 Speed
    {
        get => speed;
    }
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    private bool goingToEnd = true;

    void Start()
    {
        start.parent = null;
        end.parent = null;

        if (speed.x < 0 || speed.y < 0) goingToEnd = false;
    }

    void Update()
    {
        CheckBoundaries();

        transform.position = new Vector2(transform.position.x + speed.x * Time.deltaTime,
            transform.position.y + speed.y * Time.deltaTime);
    }

    private void CheckBoundaries()
    {
        if (goingToEnd &&
            (transform.position.x > end.position.x || transform.position.y > end.position.y))
        {
            Flip();
        }
        else if (!goingToEnd &&
                 (transform.position.x < start.position.x || transform.position.y < start.position.y))
        {
            Flip();
        }
    }

    private void Flip()
    {
        goingToEnd = !goingToEnd;
        speed *= -1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "PlayerBody") return;

        other.GetComponent<Movement>().CurrentPlatform = this;
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "PlayerBody") return;

        other.GetComponent<Movement>().CurrentPlatform = null;

    }
}
