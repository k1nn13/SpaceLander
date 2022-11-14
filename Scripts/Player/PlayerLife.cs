using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;




public class PlayerLife : MonoBehaviour
{
    //--------------------------
    [Header("Damage Settings")]
    [SerializeField][Range(0, 20)] float projectileDamage = 10;
    [SerializeField][Range(0, 20)] float collisionDamage = 10;
    [SerializeField][Range(0, 5)] float damageMultiplyer = 2.5f;
    [SerializeField][Range(0, 5)] float damageThreshold = 2;

    [SerializeField] ParticleSystem psExplosion;
    public float _speed;
    private float playerHealth = 100;
    private float maxHealth = 100;
    private float _fuelLevel;

    //--------------------------
    [Header("Player Logic Checks")]
    public bool dead = false;
    private bool isTopDamage;
    private Collider checkCollider;
    [SerializeField] bool isExplosion = true;

    //--------------------------
    [Header("Reference")]
    public setUI healthBar;

    //------------------------------
    private void Start()
    {
        playerHealth = maxHealth;
        healthBar.SetMaxValueFloat(maxHealth);
    }

    //------------------------------
    private void Update()
    {
        // get variables from PlayerMovement Script
        _fuelLevel = gameObject.GetComponent<PlayerFuel>().fuelLevel;
        _speed = gameObject.GetComponent<PlayerMovement>().speed;

        // send float to ui
        healthBar.SetValueFloat(playerHealth);

        // collision damage relevant to speed
        collisionDamage = Mathf.Round(_speed) * damageMultiplyer;

        if (isTopDamage && playerHealth >= 0)
        {
            playerHealth -= 10;
        }

        // if player.y is too low call player death
        if (transform.position.y < -5f && !dead)
        {
            Die();
        }

        // if run out of fuel die
        if (_fuelLevel == 0 && !dead)
        {
            Die();
        }

        if (playerHealth <= 0 && !dead)
        {
            Die();
        }

        

        if (dead && isExplosion)
        { 
            Instantiate(psExplosion, transform.position, transform.rotation);
            isExplosion = false;
        }
    }

    //------------------------------
    private void OnTriggerEnter(Collider other)
    {
        //checks if the player has been hit by a projectile and reduce health or call death
        if (other.gameObject.CompareTag("projectile")) 
        {
            // check if player has health
            // reduce health
            if (playerHealth >= 0)
            {
                playerHealth -= projectileDamage;
            }


        } 
    }
    
    //------------------------------
    private void OnCollisionEnter(Collision collision) {

        // check the top collider
        checkCollider = collision.GetContact(0).thisCollider;
        if (checkCollider != null)
        {
            if (checkCollider.name == "Top")
            {
                isTopDamage = true;
            }  
        }


        if (collision.gameObject.layer == 6 && _speed > damageThreshold) 
        {
            // here we check if the player hit the ground and compare with the players velocity
            playerHealth -= collisionDamage;
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        // check the top collider
        isTopDamage = false;
    }

    //------------------------------
    void TakeDamage()
    {

    }

    //------------------------------
    void Die()
    {
        // on death remove mesh stop movement and reload level
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMovement>().enabled = false;
        transform.Find("MLander").GetComponent<MeshRenderer>().enabled = false;
        Invoke(nameof(ReloadLevel), 5f);

        dead = true;
    }

    //------------------------------
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
