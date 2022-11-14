using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject target;
    private Vector3 pos, targetPos;
    [SerializeField]
    [Range(-10, -20)]
    float zPos = -10f;
    [SerializeField]
    [Range(-10, 10)]
    float xOffset, yOffset;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        targetPos = target.transform.position;

        transform.LookAt(target.transform.position);
        transform.position = new Vector3(targetPos.x + xOffset, targetPos.y + yOffset, zPos);
    }
}
