using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject GameCam;
    [SerializeField] GameObject PauseCam;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Region").GetComponent<TextMeshProUGUI>().SetText($"{PhotonNetwork.CloudRegion}");
        GameObject.Find("Ping").GetComponent<TextMeshProUGUI>().SetText($"{PhotonNetwork.GetPing()} ms");
    }

    public void Resume(){
        PauseCam.SetActive(false);
        GameObject.Find("PauseMenu").SetActive(false);
        GameCam.SetActive(true);
    }

    public void HowToRedirect(){
        Application.OpenURL("https://github.com/NotAddison/GPN_ASG2#how-to-play");
    }

    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
