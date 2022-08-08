using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Miner : BaseGoblin
{
    public override int Cost() => 2;
    public override void UseAbility(InputAction.CallbackContext context)
    {
    }
    public override void UsePassive()
    {
        // [TODO] Double gold from mine when on mine
    }
}
