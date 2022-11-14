using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarrier : MonoBehaviour
{
    [SerializeField] LineRenderer lr;
    
    [SerializeField] bool _doorOpen;
    [SerializeField] Collider col;
    [SerializeField] ParticleSystem psFront;
    [SerializeField] ParticleSystem psRear;
    [SerializeField] float gravityMax;
    [SerializeField] float gravityAmount = 0.3f;
    [SerializeField] float _timeLeft;
    [SerializeField] float _totalTime;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        //psFront = GetComponentInChildren<ParticleSystem>();
        //psRear = GetComponentInChildren<ParticleSystem>();
        lr = GetComponentInChildren<LineRenderer>();
        lr.alignment = LineAlignment.TransformZ;  
    }

    //custom map function to range values
    //---------------------------------------------------------------------
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }


    private void Update()
    {
        _doorOpen = gameObject.GetComponentInChildren<LaserReciever>().doorOpen;
        _timeLeft = gameObject.GetComponentInChildren<LaserReciever>().timeLeft;
        _totalTime = gameObject.GetComponentInChildren<LaserReciever>().totalTime;

        var gravityFront = psFront.main; // Stores the module in a local variable
        var gravityRear = psRear.main; // Stores the module in a local variable

        if (_doorOpen)
        {
            col.isTrigger = true;
            lr.enabled = false;
            gravityMax = map(_timeLeft,_totalTime, 0, 0.3f, 10f);
            gravityFront.gravityModifier = gravityMax;
            gravityRear.gravityModifier = gravityMax;

        } 
        else
        {
            col.isTrigger = false;
            lr.enabled = true;
            gravityFront.gravityModifier = gravityAmount;
            gravityRear.gravityModifier = gravityAmount;
        }
    }

}
