using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class MainMenu : MonoBehaviour
{
    void Awake(){
        if (GameObject.Find("BuildVer") != null)
        {
            GameObject.Find("BuildVer").GetComponent<TextMeshProUGUI>().SetText($"{Application.version}");
        }

        if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
    }

    public void Play(){
        SceneManager.LoadScene("Connecting");
    }

    public void Settings(){
        SceneManager.LoadScene("Settings");
    }

    public void Tutorial(){
        SceneManager.LoadScene("Tutorial");
    }

    public void Tutorial2(){
        SceneManager.LoadScene("Tutorial2");
    }

    public void Tutorial3(){
        SceneManager.LoadScene("Tutorial3");
    }

    public void Credits(){
        SceneManager.LoadScene("Credits");
    }

    public void Quit(){
        Application.Quit();
    }

    public void ReturnToMain(){
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
