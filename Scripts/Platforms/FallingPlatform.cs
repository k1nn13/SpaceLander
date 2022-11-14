using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //Debug.Log("Contact");
        if (collision.gameObject.name == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
           // GetComponent<Collider>().attachedRigidbody.AddForce(0, 1, 0);
        }

    }
}
