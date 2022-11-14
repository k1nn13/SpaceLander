using UnityEngine;

public class IdleState : TurretState
{
    //------------------------------
    public override void Update()
    {
        parent.Animator.SetBool("Shoot", false);


        //--- check the turrets current rotation and return to default rotation
        if (parent.ResetRotation.rotation != parent.Head.rotation)
        {
            lerpRotation(parent.ResetRotation, parent.Head);
            parent.TargetCheck.transform.position = parent.Head.position;
        } 

        //--- if the target is in range but not visible allow the target to be visible (retriggers 'Is Targetable'
        if (parent.Target != null)
        {

            //--- raycast to check if player is still visible
            int layerMask = 1 << 9; // add layer mask to prevent raycast from interacting with other colliders
            layerMask = ~layerMask; // this is not working yet

            //--- raycast to check if player is visible
            RaycastHit hit;
            Vector3 dir = (parent.Target.transform.position - parent.Head.transform.position).normalized;
            if (Physics.Raycast(parent.Head.transform.position, dir, out hit, Mathf.Infinity))
            {
                // check if the raycast hit the player
                if (hit.transform.gameObject.name == "Player")
                {
                    parent.IsTargetable = true;
                }
                else 
                {
                    parent.IsTargetable = false;
                }

                // if target is in range but not visible draw a line to the target
                if (parent.IsInRange && !parent.IsTargetable)
                {
                    Debug.DrawRay(parent.Head.transform.position, dir * hit.distance, Color.white);
                }
            }
        }

        //--- if target is in range and target is visible change state
        if (parent.IsInRange && parent.IsTargetable )
        {
            parent.ChangeState(new FindTargetState());  // change the state 
        }

    }

    //------------------------------
    public override void OnTriggerEnter(Collider other)
    {
       //--- if target collides with box collider (target is in range)
       if (other.tag == "Player")
        {
            //--- set target to the player transform postion
            parent.Target = other.transform; 
            parent.IsInRange = true;                      
        }
    }

    //------------------------------
    public override void OnTriggerExit(Collider other)
    {
        //--- if target leaves box collider (target out of range)
        if (other.tag == "Player")
        {
            parent.IsInRange = false;
        }
    }
}
