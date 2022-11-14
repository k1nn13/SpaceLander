using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState  
{
    protected Turret parent; // reference for each state to access the turret.


    protected float targetDist;
    protected Vector3 scaledDist;
    //--------------------------------------
    public virtual void Enter(Turret parent)
    {
        this.parent = parent;
    }

    //--------------------------------------
    public virtual void Update()
    {

    }

    //--------------------------------------
    public virtual void Exit()
    {

    }

    //--------------------------------------
    public virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void OnTriggerExit(Collider other)
    {

    }

    public virtual void lerpRotation(Transform target, Transform current)
    {
        Vector3 relativePos = (target.position) - current.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        Quaternion headRotation = Quaternion.Lerp(current.transform.rotation, toRotation, parent.AimSpeed * Time.deltaTime);
        current.transform.rotation = headRotation;
    }

    public virtual bool  lockOnDistance(Vector3 targetPosition, Transform target, float targetDistance)
    {
        float dist = Vector3.Distance(target.transform.position, targetPosition);
        if (dist < targetDistance)
        { 
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void drawDebugRay()
    {

    }

}
