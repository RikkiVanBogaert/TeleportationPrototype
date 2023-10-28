using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    public Rigidbody2D rb;

    [SerializeField] private float speed = 5f;
    private bool goingRight;

    [SerializeField] private GameObject button;

    void Start()
    {
        leftPoint.parent = null;
        rightPoint.parent = null;

        if (speed > 0)
        {
            goingRight = true;
        }
        else
        {
            FlipSprite();
            FlipButton();

            goingRight = false;
        }
    }

    void Update()
    {
        CheckBoundaries();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void CheckBoundaries()
    {
        if (transform.position.x <= leftPoint.position.x && !goingRight)
        {
            Flip();
            FlipButton();
        }

        if (transform.position.x >= rightPoint.position.x && goingRight)
        {
            Flip();
            FlipButton();
        }

    }

    private void Flip()
    {
        speed *= -1;
        goingRight = !goingRight;

        FlipSprite();
    }

    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void FlipButton()
    {
        Vector3 localScale = button.transform.localScale;
        localScale.x *= -1;
        button.transform.localScale = localScale;
    }
}
