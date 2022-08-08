using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Marauder : BaseGoblin
{
    public string code;
    public string skillName;
    public string description;
    public override int Cost() => 7;

    public override void UseAbility(InputAction.CallbackContext context)
    {
        if (Cooldown == 0){
            Cooldown += 4;
            LocalInventory.getInstance().GetGoblins().ForEach(goblin => {
                goblin.GetComponent<BaseGoblin>().Health += 1;
                goblin.GetComponent<BaseGoblin>().Damage += 1;
            });
        }
    }
}