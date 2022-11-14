using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Turret : MonoBehaviour
{
 
    protected TurretState currentState;
    [SerializeField] public Transform Target { get; set; }

    //--------------

    [Header("Turret Target References")]
    [SerializeField]
    private Transform head;
    public Transform Head { get => head; set => head = value; }
    //--------------
    [SerializeField]
    private Transform targetCheck;
    public Transform TargetCheck { get => targetCheck; set => targetCheck = value; }
    //--------------
    [SerializeField]
    private Transform resetRotation;
    public Transform ResetRotation { get => resetRotation; set => resetRotation = value; }
    //--------------
    [SerializeField]
    private Transform gunBarrel;
    public Transform GunBarrel { get => gunBarrel; set => gunBarrel = value; }
    //--------------
    [SerializeField]
    private GameObject projectile;
    public GameObject Projectile { get => projectile; set => projectile = value; }
    //--------------
    [SerializeField] private LayerMask layermask;
    //------------
    [SerializeField] PlayerLife playerLife;
    //--------------
    [Header("Turret Animation References")]
    [SerializeField]
    private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    //--------------

    [Header("Aim Speed Settings")]
    [SerializeField]
    [Range(0.01f, 10f)]
    private float aimSpeed = 2f;
    public float AimSpeed { get => aimSpeed; set => aimSpeed = value; }



    //------------- may not need this code insteatd access animation length to set fire rate
    [Header("Fire Rate Settings")]

    [SerializeField] private float fireRate;
    public float FireRate { get => FireRate; set => FireRate = value; }
    [SerializeField] private float nextShot;
    public float NextShot { get => NextShot; set => NextShot = value; }
    [SerializeField] private float counter;
    public float Counter { get => counter; set => counter = value; }

    [SerializeField] private float resetRate;
    public float ResetRate { get => resetRate; set => resetRate = value; }

    //--------------
    //[SerializeField]
    //private Vector3 aimOffset;
    //public Vector3 AimOffset { get => aimOffset; set => aimOffset = value; }

    [Header("Logic Check")]
    [SerializeField]
    private bool isInRange;
    public bool IsInRange { get => isInRange; set => isInRange = value; }
    //--------------
    [SerializeField]
    private bool isTargetable;
    public bool IsTargetable { get => isTargetable; set => isTargetable = value; }

    [SerializeField] bool isPlayerDead;
    public bool IsPlayerDead { get => isPlayerDead; set => isPlayerDead = value; }

    //[SerializeField] bool isShooting;
    //public bool IsShooting { get => isShooting; set => isShooting = value; }

    //--------------
    [SerializeField]
    private RaycastHit hit;
    public RaycastHit Hit { get => hit; set => hit = value; }





    //------------------
    private void Start()
    {   

        ChangeState(new IdleState());
    }

    //------------------
    private void Update()
    {
        currentState.Update();

        if (playerLife != null)
        {
           IsPlayerDead = playerLife.dead;
        }
  
    }

    //------------------------------
    public void Shoot()
    {   
        //shot a projectile at the target
        Quaternion headingDirection = Quaternion.FromToRotation(Projectile.transform.forward, GunBarrel.forward);
        Instantiate(Projectile, GunBarrel.position, headingDirection).GetComponent<Projectile>().Direction = GunBarrel.forward;
    }

    //------------------------------
    // may need to refactor this function - currently it is not used
    public bool CanSeeTarget(Vector3 origin, Vector3 direction, RaycastHit hit, string tag)
    {
        // check to see if the player is visible using raycast and return true / false
        if(Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == tag)
            {
                return true;
            }
        }
        
        return false;        
    } 

    //------------------
    public void ChangeState(TurretState newState)
    {
        if (newState != null)
        {
            newState.Exit();
        }

        this.currentState = newState;
        newState.Enter(this);
    }

    //------------------
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    //------------------
    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }
}
