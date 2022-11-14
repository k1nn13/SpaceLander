using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class PlayerMovement : MonoBehaviour
{

    //--------------------------------------
    [Header("Player Acceleration and Rotation")]
    [SerializeField][Range(0.1f, 4f)] float accSpeed = 0.5f;
    [SerializeField] float rotationSpeed = 1.2f;
    [SerializeField][Range(0.1f, 2f)] float landingSpeed = 2;
    public float thrustInPercent;                               // calculate percent of thrust to use in particle system
    public float speed;                                         // used to check if moving too fast
   
    public float horizontalInput;
    private float thrust;
    private Vector3 m_EulerAngleVelocity;

    //--------------------------
    [Header("Player Logic Checks")]
    public bool isThrust = false;     // check for thrust input
    public bool isRotation = false;   // check rotational input   
    public bool isGrounded = false;   // check if both legs are touching the ground
    [SerializeField] private bool isLeft = false;      // check if left leg is touching the ground
    [SerializeField] private bool isRight = false;     // check if right leg is touching the ground   
    private bool isLandingSpeed = false;
    private bool isLandingZone = false;

    [SerializeField] bool _isFuel;
    [SerializeField] bool _dead;

    //--------------------------
    [Header("Reference")]
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] LayerMask ground;
    Rigidbody rb;

    [SerializeField] private AudioSource thrustIgnitionFX;
    [SerializeField] private AudioSource rcs;

    //--------------------------


    //---------------------------------------------------------------------
    void Start()
    {
       // place handle on player to only call rigid body once
       rb = GetComponent<Rigidbody>();

    }

    //custom map function to range values
    //---------------------------------------------------------------------
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    //---------------------------------------------------------------------
    void FixedUpdate()
    {
        //------------------------
        //Apply thrust on up vector
        if (_isFuel && !_dead)
        {
            thrust = Input.GetAxisRaw("Jump");
            rb.AddForce(transform.up * accSpeed * thrust);
            thrustInPercent = map(thrust, 0, 1, 0, 2.5f);
        }
        else if (!_isFuel || _dead)
        {
            thrustInPercent = 0;
        }


        //--------------
        speed = rb.velocity.magnitude;     // use this method to check velocity of the player

        if (!isGrounded)
        {
            if (_isFuel && !_dead)
            {
                //------------Rotation
                horizontalInput = Input.GetAxis("Horizontal");
                m_EulerAngleVelocity.z += horizontalInput * -1;
                Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime * rotationSpeed);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

        }



    }

    private void Update()
    {
        


        _isFuel = gameObject.GetComponent<PlayerFuel>().isFuel;
        _dead = gameObject.GetComponent<PlayerLife>().dead;

        // used to animate particle thrust and check if refuelling can begin
        if (thrust > 0) { isThrust = true; }
        if (thrust <= 0) { isThrust = false; }


        //Audio
        if (isThrust)
        {
            if (!thrustIgnitionFX.isPlaying)
            {
                thrustIgnitionFX.Play();
            }


        }

        if (!isThrust || _dead)
        {
            thrustIgnitionFX.Stop();
        }


        if (isRotation)
        {
            if (!rcs.isPlaying)
            {
                rcs.Play();
            }
        }

        if (!isRotation || _dead)
        {
            rcs.Stop();
        }


        //------------------------
        if (isThrust == false && !_dead)
        {
            // reset thrust percent to animate particle system when no thrust applied
            thrustInPercent = map(Input.GetAxis("Jump"), 0, 1, 0, 0.5f);
        }

        // use to apply fuel consumtion on rotational thrust
        // and check if refuelling can begin
        if (horizontalInput > 0 || horizontalInput < 0)
        {
            isRotation = true;
        }
        else
        {
            isRotation = false;
        }



        // check player speed, is it safe to land
        if (rb.velocity.magnitude < landingSpeed)
        {
            isLandingSpeed = true;
        }
        else
        {
            isLandingSpeed = false;
        }

        // check player speed against and landing zone
        if (isLandingSpeed && isLandingZone)
        {

            // check if both legs are touching the ground and thrust is not active to prevent movement on the ground
            isLeft = Physics.CheckSphere(groundCheckLeft.position, .01f, ground);
            isRight = Physics.CheckSphere(groundCheckRight.position, .01f, ground);

            if (isLeft && isRight && !isThrust)
            {
                isGrounded = true;
                rb.isKinematic = true;
                m_EulerAngleVelocity.z = 0;
            }
            else
            {
                isGrounded = false;
                rb.isKinematic = false;
            }
        }


    }

    //------------------------------
    public bool IsGroundedLeft()
    {
        return Physics.CheckSphere(groundCheckLeft.position, .1f, ground);
    }

    public bool IsGroundedRight()
    {
        return Physics.CheckSphere(groundCheckRight.position, .1f, ground);
    }

    //------------------------------
    private void OnTriggerEnter(Collider other)
    {
        // checks if the player has been hit by a projectile
        if (other.gameObject.CompareTag("projectile")) 
        {
              //if hit detected apply force on player
              //Vector3 pos = gameObject.transform.position;
              //rb.AddExplosionForce(1f, pos, 10f, 3.0F, ForceMode.VelocityChange);
        }

        if (other.gameObject.CompareTag("Speed Check"))
        {
            isLandingZone = true;
        } 
    }

    //------------------------------
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Speed Check"))
        {
            isLandingZone = false;
        }

    }


}


