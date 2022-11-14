using System.Threading;
using UnityEngine;

public class FindTargetState : TurretState
{
    //------------------------------
    public override void Update()
    {
        parent.Animator.SetBool("Shoot", false);


        int layerMask = 1 << 9; // add layer mask to prevent raycast from interacting with other colliders
        layerMask = ~layerMask;
        //--- raycast to check if player is visible
        RaycastHit hit;
        Vector3 dir = (parent.Target.transform.position - parent.Head.transform.position).normalized;
        if (Physics.Raycast(parent.Head.transform.position, dir, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == "Player")
            {
                parent.IsTargetable = true;
            }
            else
            {
                parent.IsTargetable = false;
            }
        }

        //--- check if in range and is visible
        if (parent.IsInRange && parent.IsTargetable && !parent.IsPlayerDead)
        {
            //--- rotate turret head towards target
            lerpRotation(parent.Target, parent.Head);

            //--- draw debug target line form turret head to player postion
            targetDist = Vector3.Distance(parent.Target.transform.position, parent.Head.transform.position);
            scaledDist = Vector3.Scale(parent.Head.transform.forward, new Vector3(targetDist, targetDist, targetDist));
            Debug.DrawRay(parent.Head.transform.position, scaledDist, Color.blue);

            //--- move empty object towards the target then check distance to enable shoot to be called
            Vector3 targetMag = parent.Head.transform.position + scaledDist;
            parent.TargetCheck.transform.position = targetMag;

            //--- Check if the turret has fully rotated towards the target before switching to shoot mode
            bool isLocked = lockOnDistance(parent.TargetCheck.transform.position, parent.Target, .5f);
            if (isLocked)
            {
                parent.ChangeState(new ShootState());
            }
        } 
        else if (!parent.IsInRange && !parent.IsTargetable || !parent.IsTargetable || !parent.IsInRange)
        {
            //--- if target out of range AND / OR not visible return to idle state
            parent.ChangeState(new IdleState());
        }

        if (parent.IsPlayerDead)
        {
            parent.ChangeState(new IdleState());
        }

    }

    //------------------------------
    public override void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //parent.Target = null;
            //parent.ChangeState(new IdleState());
            parent.IsInRange = false;
        }
    }
}
