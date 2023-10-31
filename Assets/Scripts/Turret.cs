using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float timeOn;
    [SerializeField] private float timeOff;
    [SerializeField] private bool isOn;

    [SerializeField] private GameObject vision;

    private float timePassed;

    void Start()
    {
        if(!isOn)
        {
            vision.SetActive(false);
        }
    }
    void Update()
    {
        timePassed += Time.deltaTime;

        if (isOn)
        {
            if (timePassed >= timeOn)
            {
                SwitchActive();
            }
        }
        else
        {
            if (timePassed >= timeOff)
            {
                SwitchActive();
            }
        }
    }

    private void SwitchActive()
    {
        timePassed = 0;
        isOn = !isOn;
        vision.SetActive(isOn);
    }
}
