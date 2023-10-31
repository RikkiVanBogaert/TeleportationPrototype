using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    [SerializeField] private float speed = 8f; 
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private AgainstWall collAgainstWall;

    private Platform currentPlatform;

    public Platform CurrentPlatform
    {
        get { return currentPlatform; }
        set { currentPlatform = value; }
    }

    public bool IsFacingRight
    {
        get => isFacingRight;
    }
    
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0)
        {
            Flip();
        }

        MoveOnPlatform();

        //KillY
        if (transform.position.y < -8)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void MoveOnPlatform()
    {
        if (!currentPlatform) return;

        transform.position = new Vector2(transform.position.x + currentPlatform.Speed.x * Time.deltaTime,
            transform.position.y + currentPlatform.Speed.y * Time.deltaTime);
    }
   
}
