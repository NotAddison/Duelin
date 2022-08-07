using System.ComponentModel;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TurnManager : MonoBehaviour
{
    public static TurnManager getInstance() => GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
    private static Player CurrentPlayer = PhotonNetwork.MasterClient;  // Default to Master Client to be the first player
    private int turnNumber = 0;

    public bool CheckTurn() => CurrentPlayer == PhotonNetwork.LocalPlayer;

    [PunRPC]
    public void StartTurn()
    {
        if (!CheckTurn()) return;
        turnNumber += 1;
        if (turnNumber <= 1) FirstTurn();

        LocalInventory.getInstance().UpdateGoldAmount();
        GameObject.FindWithTag("GoldAmount").GetComponent<GoldBar>().RenderAmount();
    }

    [PunRPC]
    public void EndTurn()
    {
        CurrentPlayer = CurrentPlayer.GetNext();
        StartTurn();
    }

    private void FirstTurn()
    {
        //!Open shop or smt idk
    }

    public void HandleTurnAction(ACTION action)
    {
        string ACTION = Utility.GetEnumDescription(action);
        PhotonView.Get(this).RPC(ACTION, RpcTarget.All);
    }

    public enum ACTION
    {
        [Description("EndTurn")]
        END,
        [Description("StartTurn")]
        START
    }
}