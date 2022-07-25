using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlaySingle(){
        SceneManager.LoadScene("Test");
    }

    public void PlayMulti(){
        SceneManager.LoadScene("Connecting");
    }

    public void Quit(){
        Application.Quit();
    }

    public void ReturnToMain(){
        SceneManager.LoadScene("MainMenu");
    }
}
