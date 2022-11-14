using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFuel : MonoBehaviour
{
    //--------------------------
    private float fuelCapacity = 100;
    private bool _isThrust = false;
    private bool _isRotation = false;
    [SerializeField] private bool isRefueling = false;
    [SerializeField] private bool _isGrounded;
    //[SerializeField] private bool isContact;
    [SerializeField] public bool isFuel;

    //--------------------------
    [Header("Fuel Settings")]
    [SerializeField][Range(0,30)] float fuelRate;
    public float fuelLevel = 100;  //accesed by player life to call death
    //--------------------------
    [Header("References")]
    public setUI fuelBar;

    //--------------------------
    private void Start()
    {
        fuelLevel = fuelCapacity;
        fuelBar.SetMaxValueFloat(fuelCapacity);
    }

    //------------------------------
    private void Update()
    {
        // get variables from PlayerMovement Script
        _isThrust = gameObject.GetComponent<PlayerMovement>().isThrust;
        _isRotation = gameObject.GetComponent<PlayerMovement>().isRotation;
        _isGrounded = gameObject.GetComponent<PlayerMovement>().isGrounded;

        if (_isThrust) { ConsumeFuel(fuelRate); }
        if (_isRotation) { ConsumeFuel(fuelRate*.2f); }
        if (isRefueling) { StartRefuelling(10f); }

        fuelBar.SetValueFloat(fuelLevel);


        if (fuelLevel <= 0)
        {
            isFuel = false;
        }

        if (fuelLevel > 0)
        {
            isFuel = true;
        }
    }

    //------------------------------
    // function to reduce the amount if fuel if thrust is applied
    // level of fuel consumption should differ between main thruster and rotational thruster
    void ConsumeFuel(float speed)
    {
        if (fuelCapacity > 0)
        {
            fuelLevel -= speed * Time.deltaTime;
            if (fuelLevel < 0)
            {
                fuelLevel = 0;
            }      
        }
    }

    //------------------------------
    void StartRefuelling(float speed)
    {
        if (fuelLevel <= fuelCapacity )
        {
            fuelLevel += speed * Time.deltaTime;
            if (fuelLevel > fuelCapacity)
            {
                fuelLevel = fuelCapacity;
            }   
        }
    }

    //------------------------------
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Refuel Pad" && _isGrounded)
        {
            isRefueling=true;
        }
    }

    //------------------------------
    private void OnTriggerExit(Collider collision)
    {
        if (!_isGrounded)
        {
            isRefueling = false;
        }
    }

}
