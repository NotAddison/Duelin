using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : BaseGoblin
{
    public override int Cost() => 1;
    void Start() {
        LocalInventory.getInstance().GetGoblins().ForEach(x => {
            if (x.GetType() == typeof(Soldier)) {
                LocalInventory.getInstance().AddGold(1);
            }
            else if (x.GetType() == typeof(Captain)) {
                LocalInventory.getInstance().AddGold(1);
            }
            else if (x.GetType() == typeof(Warrior)) {
                LocalInventory.getInstance().AddGold(1);
            }
        });
    }
}
