using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerMovement playerScript;
    private bool _isGrounded;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        _isGrounded = playerScript.isGrounded;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && _isGrounded)
        {
            Invoke("EndLevel", 2f);
        }
    }

    public void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
