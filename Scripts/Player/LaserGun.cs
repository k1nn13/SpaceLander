using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaserGun : MonoBehaviour
{
    [Header("Laser Reference")]
    public Transform firePoint;
    public GameObject laserPrefab;
    private GameObject spawnLaser;
    public LineRenderer linePos;


    public Transform firePointL;
    public Transform firePointR;

    public GameObject laserPrefabL;
    public GameObject laserPrefabR;

    private GameObject spawnLaserL;
    private GameObject spawnLaserR;

    public LineRenderer linePosL;
    public LineRenderer linePosR;

    public LayerMask mask;
    RaycastHit hit;
    Rigidbody rb;





    [Header("Laser Settings")]
    [SerializeField][Range(0, 20)] float maxDist = 2f;
    [SerializeField][Range(0, 100)] float hitForce = 5;
    float targetDist;
    //string hitTag;

    [SerializeField] public int directionMode = 0;

    [SerializeField] public int beamMode = 0;



    //---------------
    private void Start()
    {
        //spawnLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        //linePos = spawnLaser.gameObject.GetComponentInChildren<LineRenderer>();
        //linePos.SetPosition(0, firePoint.TransformDirection(Vector3.right) * maxDist);
        //DisableLaserR();
        //linePos.SetPosition(1, new Vector3(0, 0, 0f));


        spawnLaserR = Instantiate(laserPrefabR, firePointR.transform) as GameObject;
        linePosR = spawnLaserR.gameObject.GetComponentInChildren<LineRenderer>();
        DisableLaserR();

        spawnLaserL = Instantiate(laserPrefabL, firePointL.transform) as GameObject;
        linePosL = spawnLaserL.gameObject.GetComponentInChildren<LineRenderer>();
        DisableLaserL();
    }

    //---------------
    void EnableLaserR()
    {
        spawnLaserR.SetActive(true);
    }

    //---------------
    void DisableLaserR()
    {
        spawnLaserR.SetActive(false);
    }

    //---------------
    void UpdateLaserR()
    {
        if(firePointR != null)
        {
            spawnLaserR.transform.position = firePointR.transform.position;
            //Vector3 target = firePoint.TransformDirection(Vector3.right) * 2f;
            //transform.TransformDirection(Vector3.right);
        }
    }

    //---------------
    void EnableLaserL()
    {
        spawnLaserL.SetActive(true);
    }

    //---------------
    void DisableLaserL()
    {
        spawnLaserL.SetActive(false);
    }

    //---------------
    void UpdateLaserL()
    {
        if (firePointL != null)
        {
            spawnLaserL.transform.position = firePointL.transform.position;
            //Vector3 target = firePoint.TransformDirection(Vector3.right) * 2f;
            //transform.TransformDirection(Vector3.right);
        }
    }


    //---------------
    private void Update()
    {
        // reset the laser when key released
        if (!Input.GetKey("f"))
        {
            DisableLaserR();
            DisableLaserL();
            targetDist = 0;
        }


        if (Input.GetKeyDown("g"))
        {
            directionMode += 1;
        }

        if (directionMode > 1)
        {
            directionMode = 0;
        }


        if (Input.GetKeyDown("v"))
        {
            beamMode += 1;
        }

        if (beamMode > 1)
        {
            beamMode = 0;
        }
    }

    //---------------
    void FixedUpdate()
    {


        if (directionMode == 0)
        {

            if (Input.GetKey("f"))
            {

                EnableLaserR();
                UpdateLaserR();

                if (Physics.Raycast(firePointR.position, firePointR.right, out hit, maxDist, mask))
                {
                    //Debug.Log(hit.transform.gameObject);
                    //linePos.SetPosition(1, new Vector3(0, 0, Mathf.Lerp(0, hit.distance, 2.5f * Time.deltaTime)));
                    //Debug.Log("Target Name: " + hit.transform.gameObject.tag);
                    if (hit.distance < maxDist)
                    {
                        targetDist = hit.distance;
                    }

                    //hitTag = hit.transform.gameObject.tag;

                    //check if we hit reciever to open the door
                    if (hit.transform.gameObject.tag == "Reciever")
                    {
                        hit.transform.SendMessage("OpenDoor");
                        //hit.transform.SendMessage("ApplyDamage", null, SendMessageOptions.DontRequireReceiver);
                    }

                    if (hit.rigidbody != null)
                    {
                        Vector3 rayPoint = hit.point;
                        Vector3 rayOrigin = firePointR.position;
                        //Vector3 dir = rayOrigin - rayPoint;
                        Vector3 dir = rayPoint - rayOrigin;
                        //hit.rigidbody.AddForce(dir * hitForce);

                        if (beamMode == 0)
                        {
                            hit.rigidbody.AddForceAtPosition(dir.normalized * hitForce, hit.rigidbody.transform.position);
                        }

                        if (beamMode == 1)
                        {
                            hit.rigidbody.AddForceAtPosition(dir.normalized * -hitForce, hit.rigidbody.transform.position);
                        }
                       
                    }
                }
                else
                {
                    targetDist = 0.5f;
                    //hitTag = null;
                }

                // set the position of the laser
                linePosR.SetPosition(1, new Vector3(0, 0, targetDist));
            }

        }



        if (directionMode == 1)
        {
            if (Input.GetKey("f"))
            {

                EnableLaserL();
                UpdateLaserL();


                if (Physics.Raycast(firePointL.position, firePointL.right, out hit, maxDist, mask))
                {
                    //Debug.Log(hit.transform.gameObject);
                    //linePos.SetPosition(1, new Vector3(0, 0, Mathf.Lerp(0, hit.distance, 2.5f * Time.deltaTime)));
                    //Debug.Log("Target Name: " + hit.transform.gameObject.tag);
                    if (hit.distance < maxDist)
                    {
                        targetDist = hit.distance;
                    }

                    //check if we hit reciever to open the door
                    if (hit.transform.gameObject.tag == "Reciever")
                    {
                        hit.transform.SendMessage("OpenDoor");
                    }

                    if (hit.rigidbody != null)
                    {
                        Vector3 rayPoint = hit.point;
                        Vector3 rayOrigin = firePointL.position;
                        //Vector3 dir = rayOrigin - rayPoint.normalized;
                        //hit.rigidbody.AddForce(dir * -hitForce);

                        Vector3 dir = rayPoint - rayOrigin;
                        //hit.rigidbody.AddForce(dir * hitForce);
                        hit.rigidbody.AddForceAtPosition(dir.normalized * hitForce, hit.rigidbody.transform.position);
                    }
                }
                else
                {
                    targetDist = 0.5f;
                }

                // set the position of the laser
                linePosL.SetPosition(1, new Vector3(0, 0, targetDist));
            }
        }
    }





    



//--
}
