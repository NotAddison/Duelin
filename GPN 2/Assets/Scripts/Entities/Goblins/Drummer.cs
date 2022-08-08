using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Drummer : BaseGoblin
{
    public override int Cost() => 5;
    public override void UseAbility(InputAction.CallbackContext context)
    {
    }
    public override void UsePassive()
    {
        
    }
}
