using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TurnManager
{
    private static TurnManager _instance = null;
    public static TurnManager getInstance() => _instance == null ? _instance = new TurnManager() : _instance;
    private Player CurrentPlayer = PhotonNetwork.MasterClient;  // Default to Master Client to be the first player

    public bool CheckTurn(){
        bool status = CurrentPlayer == PhotonNetwork.LocalPlayer ? true : false;
        if (!status) Debug.LogError($"[TurnManager] It's not your turn!");
        return status;
    }

    public void EndTurn()
    {
        // Debug.LogError($"[TurnManager] EndTurn() called by {CurrentPlayer.ActorNumber}");
        Debug.LogError($"[TurnManager] Player {CurrentPlayer.ActorNumber} has ended their turn");
        CurrentPlayer =  CurrentPlayer.GetNext();
    }

    public Player GetCurrentPlayer() => CurrentPlayer;
}