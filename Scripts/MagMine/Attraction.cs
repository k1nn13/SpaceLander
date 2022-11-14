using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

[RequireComponent(typeof(Rigidbody))]
public class Attraction : MonoBehaviour
{
    //-------
    [Header("Acceleration and Rotation")]
    [SerializeField] public float rotSpeed = 1f;
    [SerializeField] private float thrust = 0;
    [SerializeField] private float acc = 2;
    [SerializeField] private float max = 2;
    private float min = 0;
    [SerializeField] private bool lookAt;

    //-------
    [Header("Timer")]
    [SerializeField] private float maxTime;
    [SerializeField] private float timeRemaining;
    [SerializeField] private bool isContact;
    [SerializeField] ParticleSystem psExplosion;
    //-------
    [Header("Reference")]
    [SerializeField] public Transform target;
    private Rigidbody rb;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (lookAt)
        {
           thrust = Mathf.Lerp(min, max, 100 * Time.deltaTime);
        }

        if(isContact)
        {
            Instantiate(psExplosion, transform.position, transform.rotation);
            isContact = false;
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (lookAt)
            {
                FollowTarget();  // rotate to face target
                rb.AddForce(transform.forward * acc * thrust); // apply force (attract to target)
            }

            if (!lookAt)
            { 
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 1.5f * Time.deltaTime);
            }
        }
    }



    private void FollowTarget()
    {
        //rotate to face target
        Vector3 relativePos = (target.position) - transform.position;  //check distance
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        Quaternion deltaRotation = Quaternion.Lerp(rb.rotation, toRotation, rotSpeed * Time.deltaTime);
        rb.MoveRotation(deltaRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            lookAt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lookAt = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Instantiate(psExplosion, transform.position, transform.rotation);
        //Destroy(gameObject);
        isContact = true;

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddExplosionForce(100f, transform.position, 5f, 3.0F);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        //Instantiate(psExplosion, transform.position, transform.rotation);
        //Destroy(gameObject);
        isContact = false;
    }




}
