using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever : MonoBehaviour
{
    [SerializeField] public bool doorOpen = false;
    [SerializeField] public float timeLeft;
    [SerializeField] public float totalTime = 5f;
    [SerializeField] bool timerOn;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            timerOn = true;
            if (timerOn)
            {
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                }
                else
                {
                   
                    timerOn = false;
                    doorOpen = false;
                    timeLeft = totalTime;

                }
            } 

        }
    }

    void OpenDoor()
    {
        //Debug.Log("I was hit by a Ray");
        doorOpen = true;
    }
}
