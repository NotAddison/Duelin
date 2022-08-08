using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreeArmored : Tank
{
    public override int Cost() => 1;
    public override void UseAbility(InputAction.CallbackContext context)
    {
        
    }
    public override void UsePassive()
    {
        base.UsePassive();
        Debug.Log("Tree Armored regenerates health");
        // [TODO] Implement max health instead of health
        if (Health < 8) Health += 1;
    }
}
