using System.ComponentModel;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TurnManager : MonoBehaviour
{
    public static TurnManager getInstance() => GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
    private static Player CurrentPlayer = PhotonNetwork.MasterClient;  // Default to Master Client to be the first player
    private int turnNumber = 0;
    public bool actionTaken, bonusActionTaken, itemPurchased, isFirstTurn = false;
    public bool CheckTurn() => CurrentPlayer == PhotonNetwork.LocalPlayer;

    [PunRPC]
    public void StartTurn()
    {
        if (!CheckTurn()) return;
        turnNumber += 1;
        if (turnNumber <= 1) actionTaken = bonusActionTaken = isFirstTurn = true;
        LocalInventory.getInstance()
            .UpdateGameState()
            .UpdateGoldAmount();
        LocalInventory.getInstance().GetGoblins().ForEach(goblin => {
            goblin.UsePassive();
            goblin.HandleStatusEffects();
        });
        itemPurchased = isFirstTurn ? false : !ShopManager.getInstance().CanAffordAny();
        EndTurnButton.getInstance().RenderButton(actionTaken && bonusActionTaken && itemPurchased);
    }

    [PunRPC]
    public void EndTurn()
    {
        actionTaken = bonusActionTaken = itemPurchased = isFirstTurn = false;
        EndTurnButton.getInstance().RenderButton(actionTaken && bonusActionTaken && itemPurchased);
        CurrentPlayer = CurrentPlayer.GetNext();
        StartTurn();
    }

    public void HandleTurnControl(TURN_CONTROL control)
    {
        string ACTION = Utility.GetEnumDescription(control);
        PhotonView.Get(this).RPC(ACTION, RpcTarget.All);
    }

    public void HandleTurnAction(ACTION action)
    {
        switch(action)
        {
            case ACTION.ACTION:
                actionTaken = true;
                break;
            case ACTION.BONUS_ACTION:
                bonusActionTaken = true;
                break;
            case ACTION.PURCHASE:
                if (isFirstTurn) itemPurchased = !ShopManager.getInstance().CanAffordAny();
                else itemPurchased = true;
                break;
            default:
                break;
        }
        EndTurnButton.getInstance().RenderButton(actionTaken && bonusActionTaken && itemPurchased);
    }

    public enum ACTION
    {
        ACTION,
        BONUS_ACTION,
        PURCHASE
    }

    public enum TURN_CONTROL
    {
        [Description("EndTurn")]
        END,
        [Description("StartTurn")]
        START
    }
}