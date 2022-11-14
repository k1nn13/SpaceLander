using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Laser : MonoBehaviour
{
    [SerializeField] LaserGun laserGun;
    [SerializeField] int _laserDirection;
    [SerializeField] int _laserAttraction;
    [SerializeField] Image dirRight;
    [SerializeField] Image dirLeft;
    [SerializeField] Image dirPush;
    [SerializeField] Image dirAttract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _laserDirection = laserGun.directionMode;
        _laserAttraction = laserGun.beamMode;


        if (_laserDirection == 0)
        {
            dirRight.enabled = true;
            dirLeft.enabled = false;
        }

        if (_laserDirection == 1)
        {
            dirRight.enabled = false;
            dirLeft.enabled = true;
        }

        if (_laserAttraction == 0)
        {
            dirPush.enabled = true;
            dirAttract.enabled = false;
        }

        if (_laserAttraction == 1)
        {
            dirPush.enabled = false;
            dirAttract.enabled = true;
        }
    }
}
