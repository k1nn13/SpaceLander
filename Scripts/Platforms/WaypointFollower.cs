using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    Vector3 minDistance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody rb;
    



    private void Update()
    {
        if (Vector3.Distance(rb.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

    }


    void FixedUpdate()
    {


        //transform.position = Vector3.MoveTowards(transform.position,
        //                                         waypoints[currentWaypointIndex].transform.position,
        //                                         speed * Time.deltaTime);

        rb.MovePosition(Vector3.MoveTowards(rb.transform.position,
                                                 waypoints[currentWaypointIndex].transform.position,
                                                 speed * Time.deltaTime));


        //platform.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(platform.position,
        //                                         waypoints[currentWaypointIndex].transform.position,
        //                                         speed * Time.deltaTime));
    }
}
