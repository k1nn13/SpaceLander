using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem psMain;

    [SerializeField]
    private float minStartSpeed, maxStartSpeed;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    PlayerLife playerLife;

    [SerializeField]
    private float thrustAmount;


    [SerializeField]
    bool _dead;


    private void Start()
    {
      psMain = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
  
        thrustAmount = gameObject.GetComponentInParent<PlayerMovement>().thrustInPercent;
        _dead = gameObject.GetComponentInParent<PlayerLife>().dead;

        if (psMain != null)
        {
            var main = psMain.main;
            main.startSpeed = thrustAmount;

        }

        if (_dead)
        {
            psMain.Stop();
        }
       
    }


       
           
            

            
    



}
