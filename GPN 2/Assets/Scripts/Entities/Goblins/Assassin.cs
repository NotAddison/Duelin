using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Assassin : BaseGoblin
{
    public override int Cost() => 2;
    public override void UseAbility(InputAction.CallbackContext context)
    {
        if (Cooldown == 0)
        {
            Cooldown += 2;
            
        }
    }
    public override void UsePassive()
    {
        
    }
}
