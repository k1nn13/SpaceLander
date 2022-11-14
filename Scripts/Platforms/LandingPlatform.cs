using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
  
    public GameObject _Player;



    //sets player object to a child of the moving platform to maintain position
    void OnCollisionEnter(Collision collision)
    {
        //transform.Find("LandingPad").GetComponent<Collider>();
        if (collision.gameObject.name == "Player")
        {
          //Debug.Log("Player Landed: Begin Refuelling ");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {

        }
    }
}
