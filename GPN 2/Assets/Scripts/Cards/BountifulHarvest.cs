using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountifulHarvest : Consumable
{
    public override int Cost() => 4;
    protected override void HandleEffect(GameObject target = null){
        LocalInventory.getInstance().AddGameState(LocalInventory.GAME_STATE.HARVEST, 2);
    }
}