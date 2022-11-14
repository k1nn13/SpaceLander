using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShootState : TurretState
{
   
    //------------------------------
    public override void Update()
    {


        //-----------------------
        if (parent.Target != null)
        {
            //--- raycast to check if player is still visible
            int layerMask = 1 << 9; // add layer mask to prevent raycast from interacting with other colliders??
            layerMask = ~layerMask;

            RaycastHit hit;
            Vector3 dir = (parent.Target.transform.position - parent.Head.transform.position).normalized;
            if (Physics.Raycast(parent.Head.transform.position, dir, out hit, Mathf.Infinity))
            {

                if (hit.transform.gameObject.name == "Player")
                {
                    parent.IsTargetable = true;   
                }
                else if (hit.transform.gameObject.name != "Player")
                {
                    parent.IsTargetable = false;
                }
            }

            //--- if in range and visible
            if (parent.IsInRange && parent.IsTargetable && !parent.IsPlayerDead)
            {
                lerpRotation(parent.Target, parent.Head); // continue to apply targeting rotation to the turret

                //--- raycast from gun barrel to target to begin shooting 
                RaycastHit shootHit;
                if (Physics.Raycast(parent.GunBarrel.transform.position, 
                                    parent.GunBarrel.TransformDirection(Vector3.forward), 
                                    out shootHit, 
                                    Mathf.Infinity
                                    ))
                {

                    //--- check what ray cast hit and begin shooting
                    //Debug.Log(hit.transform.gameObject);
                    if (shootHit.transform.gameObject.name == "Player")
                    {
                        Debug.DrawRay(parent.GunBarrel.transform.position, parent.GunBarrel.TransformDirection(Vector3.forward) * shootHit.distance, Color.red);
                        parent.Animator.SetBool("Shoot", true);
                        //parent.Animator.SetTrigger("Shoot");
                    }
                }
            }
        }

        // return to idle state if not In Range AND / OR not Targetable / or null
        if (!parent.IsTargetable)
        {
            //parent.Target = null;
            parent.ChangeState(new IdleState());
        }

        if (!parent.IsInRange)
        {
            parent.ChangeState(new IdleState());
        }

        //check if player is dead to stop targeting
        if (parent.IsPlayerDead)
        {
            parent.ChangeState(new IdleState());
        }


    }


    //------------------------------
    public override void OnTriggerEnter(Collider other)
    {
        //--- check if in range
        if (other.tag == "Player")
        {
            parent.IsInRange = true;
        }
    }

    //------------------------------
    public override void OnTriggerExit(Collider other)
    {
        //-- check if out of range
        if (other.tag == "Player")
        {
            parent.IsInRange = false;
        }
    }
}
