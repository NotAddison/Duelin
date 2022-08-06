using UnityEngine;

public class EndTurnButton : Entity, IClickable
{
    public void OnClick(Entity prevEntity = null)
    {
        if (!TurnManager.getInstance().CheckTurn()) return;
        LocalInventory.getInstance().GetGoblins().ForEach(entity => entity.actionManager.Deselect());
        TurnManager.getInstance().HandleTurnAction(TurnManager.ACTION.END);
    }

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos){ }

    public override void OnDeath(BaseGoblin attackingEntity = null, Vector3? targetPos = null){ }
}