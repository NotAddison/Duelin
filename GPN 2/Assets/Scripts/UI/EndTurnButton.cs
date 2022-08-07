using UnityEngine;

public class EndTurnButton : UIElement, IClickable
{
    public void OnClick(GameObject prevSelection = null)
    {
        LocalInventory.getInstance().GetGoblins().ForEach(entity => entity.actionManager.Deselect());
        TurnManager.getInstance().HandleTurnAction(TurnManager.ACTION.END);
    }
}