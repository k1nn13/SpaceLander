using System;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Vector3 Direction { get; set; }
    //private float reflect = 1.0f; // dont need this
    Rigidbody rb;
    private bool isContact;
    [SerializeField] ParticleSystem psSpark;

    [SerializeField] GameObject sparkObj;
    [SerializeField] Transform turret;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    //----------------------
    void Update()
    {
        if (isContact)
        {
            //.GetComponent<>().Direction =  ;
            //rotate to face target
            Vector3 relativePos = turret.position - transform.position;  //check distance
            Quaternion toRotation = Quaternion.LookRotation(relativePos);

            Instantiate(psSpark, transform.position, toRotation);
            isContact = false;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Direction * speed;
        Destroy(gameObject, 5f);    
    }

    //----------------------
    void OnTriggerEnter(Collider collision)
    {
        
        //Debug.Log(collision);
        isContact = true;

        //rb.velocity *= -1;
        //GetComponent<Rigidbody>().useGravity = true;                                       // add various forces to animate projectile bouncing after hit
        //GetComponent<Rigidbody>().AddExplosionForce(10f, transform.position, 5f, 10.0F);

        if (collision)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(1f, transform.position, 5f, 3.0F);
            }
        }

        Destroy(gameObject, 0.5f);
    }

}


