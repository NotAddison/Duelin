using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Disguised : BaseGoblin
{
    public override int Cost() => 4;
    public override void UseAbility(InputAction.CallbackContext context)
    {
        // [TODO] Implement copying other units' abilities
    }
    public override void UsePassive()
    {
        
    }
}
