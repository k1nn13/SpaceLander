using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 scale;
    [SerializeField] Vector3 m_EulerAngleVelocity;
    [SerializeField] float rotationSpeed;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        scale = new Vector3(Random.Range(1.0f, 3.0f), Random.Range(1.0f, 3.0f), Random.Range(1.0f, 3.0f));
        transform.localScale = scale;

        rotationSpeed = Random.Range(-10.0f, 10.0f);
        m_EulerAngleVelocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        velocity = new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f));
    }

    // Update is called once per frame
    void Update()
    {

       
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime * rotationSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.velocity = velocity;

    }
}
