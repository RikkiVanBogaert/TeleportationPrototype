using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class Platform : MonoBehaviour
{
    private Vector2 speed = new Vector2(0,0);

    [SerializeField] private float timeOneRun = 1f;
    [SerializeField] private bool goForward = true;
    public Vector2 Speed
    {
        get => speed;
    }
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    private float timePassed;

    void Start()
    {
        start.parent = null;
        end.parent = null;

        var dx = start.position.x - end.position.x;
        var dy = start.position.y - end.position.y;

        speed.x = dx / timeOneRun;
        speed.y = dy / timeOneRun;

        if (!goForward) speed *= -1;

        float lerpValue;

        if (dx != 0)
        {
            if (goForward)
            {
                lerpValue = Mathf.Abs(transform.position.x - start.position.x) / Mathf.Abs(dx);
            }
            else
            {
                lerpValue = Mathf.Abs(transform.position.x - end.position.x) / Mathf.Abs(dx);
            }
        }
        else
        {
            if (!goForward)
            {
                lerpValue = Mathf.Abs(transform.position.y - start.position.y) / Mathf.Abs(dy);
            }
            else
            {
                lerpValue = Mathf.Abs(transform.position.y - end.position.y) / Mathf.Abs(dy);
            }
        }

        timePassed = lerpValue * timeOneRun;
    }

    void Update()
    {
        CheckBoundaries();

        transform.position = new Vector2(transform.position.x + speed.x * Time.deltaTime,
            transform.position.y + speed.y * Time.deltaTime);
    }

    private void CheckBoundaries()
    {
        timePassed += Time.deltaTime;

        if (timePassed > timeOneRun)
        {
            timePassed = 0;
            speed *= -1;
        }
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
