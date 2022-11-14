using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    public GameObject root;        // set the transform to the root to prevent distortion of scale


       
    //------------------------------
    //sets player object to a child of the moving platform to maintain position
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("collision");
            collision.rigidbody.transform.SetParent(root.transform);
            //collision.gameObject.transform.position = transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


}
