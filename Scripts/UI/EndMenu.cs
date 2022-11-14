using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
   
        //SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
    }
}
