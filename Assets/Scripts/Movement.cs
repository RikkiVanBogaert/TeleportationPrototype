using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Abertay.Analytics;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private PlayerVision playerVision;
    private Vector3 startPosition;

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

    private int deathsToFalling;
    public int DeathsToFalling
    { get { return deathsToFalling; } }

    private int deathsToEnemies;
    public int DeathsToEnemies
    { get { return deathsToEnemies; } }

    void Start()
    {
        startPosition = transform.position;
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
            Respawn();
        }
    }

    public void Respawn(bool fellOfMap = false)
    {
        Color c = Color.red;
        c.a = 0.5f;
        AnalyticsManager.LogHeatmapEvent("PlayerDied", transform.position, c);

        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        //reset vars
        currentPlatform = null;
        if (!isFacingRight)
        {
            Flip();
        }

        playerVision.CanTeleport = false;
        playerVision.CurrentTeleport = null;

        if (fellOfMap) ++deathsToFalling;
        else ++deathsToEnemies;
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

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    public void Menu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Load main menu");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
