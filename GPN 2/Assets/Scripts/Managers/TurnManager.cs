using System.ComponentModel;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TurnManager : MonoBehaviour
{
    public int Time = 30;
    private int TurnTimer = 0;
    public static TurnManager getInstance() => GameObject.FindWithTag("GameManager").GetComponent<TurnManager>();
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
        else InvokeRepeating("CountDown", 1, 1);
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/turn_toast"), Vector3.zero, Quaternion.identity);
        LocalInventory.getInstance()
            .UpdateGameState()
            .UpdateGoldAmount();

        LocalInventory.getInstance().GetGoblins().ForEach(goblin => {
            goblin.isAbilityUsed = false;
            if(goblin is IPassiveAbility) ((IPassiveAbility) goblin).UsePassive();
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
        CancelInvoke("CountDown");
        TurnTimer = Time;
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
    
    public void CountDown(){
        if(TurnTimer <= 0){
            PhotonView.Get(this).RPC("EndTurn", RpcTarget.All);
        }
        TurnTimer--;
        // Debug.LogError($"[GameManager] Turn Timer: {TurnTimer}");
    }
}