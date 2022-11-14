using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        //var emission = psExplosion.emission; // Stores the module in a local variable
        //var main = psExplosion.main;

        //emission.enabled = false; // Applies the new value directly to the Particle
        //psExplosion.Play();
        //main.loop = false;
        ps = GetComponent<ParticleSystem>();
        ps.Play();

        var main = ps.main;
        main.loop = false;

       //ar emission = ps.emission;
       //mission.enabled = true; // Applies the new value directly to the Particle System
       ps.Stop();
        //stroy(gameObject);
    }
}
