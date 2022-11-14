using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalParticle : MonoBehaviour
{

    public ParticleSystem psLeft;
    public ParticleSystem psRight;
    public float horizontalInput;
    float leftThrust, rightThrust;
    [SerializeField] bool _isFuel;
    [SerializeField] bool _dead;


    //custom map function to range values
    //---------------------------------------------------------------------
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    // Update is called once per frame
    void Update()
    {
        _isFuel = gameObject.GetComponent<PlayerFuel>().isFuel;
        _dead = gameObject.GetComponent<PlayerLife>().dead;


        horizontalInput = Input.GetAxis("Horizontal");

        if (_isFuel && !_dead)
        {
            if (horizontalInput > 0)
            {
                var emissionLeft = psLeft.emission; // Stores the module in a local variable
                emissionLeft.enabled = true; // Applies the new value directly to the Particle


                leftThrust = map(horizontalInput, 0, 1, 0, 3.6f);


                emissionLeft.rateOverTime = leftThrust;
            }

            if (horizontalInput < 0)
            {
                var emissionRight = psRight.emission; // Stores the module in a local variable
                emissionRight.enabled = true; // Applies the new value directly to the Particle


                rightThrust = map(horizontalInput, 0, -1, 0, 3.6f);


                emissionRight.rateOverTime = rightThrust;
            }

        }
        else if (!_isFuel || _dead)
        {
            leftThrust = 0;
            rightThrust = 0;
            psLeft.Stop();
            psRight.Stop();


        }
    }
}
