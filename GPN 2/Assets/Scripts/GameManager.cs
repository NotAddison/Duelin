using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager getInstance() => GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    List<Vector3> spawnPositions = new List<Vector3>(){
        new Vector3(     0, -0.48f, 0),
        new Vector3(     0,  0.80f, 0),
        new Vector3(-1.28f,  0.16f, 0),
        new Vector3( 1.28f,  0.16f, 0),
    };

    public int amountToWin = 15;

    void Start()
    {
        // Play OST 
        if (SettingsMenu.getInstance() == null) // Did not change any Settings
        {
            FindObjectOfType<AudioManager>().Play("OST 2", 1f);
            FindObjectOfType<AudioManager>().Play("Ambience", 1f);
        }
        else
        {
            float ostVol = SettingsMenu.getInstance().GetOSTVol();
            Debug.Log($"[GameManager] OST Volume set to {ostVol}");
            FindObjectOfType<AudioManager>().Play("OST 2", ostVol);
            FindObjectOfType<AudioManager>().Play("Ambience", SettingsMenu.getInstance().GetAmbienceVol());
        }
        


        amountToWin += PhotonNetwork.CountOfPlayersInRooms > 2 ? (PhotonNetwork.CountOfPlayersInRooms - 2) * 5 : 0;
        GameObject.FindWithTag("GoldBar").GetComponent<GoldBar>().RenderBar();
        PhotonNetwork.Instantiate("Prefabs/Structures/spawn", spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber-1], Quaternion.identity);
        GameObject.FindWithTag("GoldAmount").GetComponent<GoldAmount>().RenderAmount();
        TurnManager.getInstance().StartTurn();
    }
    void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer){
        Debug.LogError($"[GameManager]: Player {otherPlayer.ActorNumber} has left the room");
    }
}