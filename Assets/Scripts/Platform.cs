using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isGoingRight = true;
    [SerializeField] private float speed = 3;
    public float Speed
    {
        get => speed;
    }
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    void Start()
    {
        start.parent = null;
        end.parent = null;

        if (speed > 0)
        {
            isGoingRight = true;
        }
        else
        {
            isGoingRight = false;
        }
    }

    void Update()
    {
        CheckBoundaries();
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    private void CheckBoundaries()
    {
        if (transform.position.x <= start.position.x && !isGoingRight)
        {
            Flip();
        }

        if (transform.position.x >= end.position.x && isGoingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        speed *= -1;
        isGoingRight = !isGoingRight;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "PlayerBody") return;

        other.GetComponent<Movement>().CurrentPlatform = this;

        //other.transform.position = new Vector2(other.transform.position.x + speed, other.transform.position.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "PlayerBody") return;

        other.GetComponent<Movement>().CurrentPlatform = null;

    }
}
