using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Shielder : Tank
{
    private BaseGoblin entity;
    public override int Cost() => 3;
    public override void UsePassive()
    {
        base.UsePassive();
    }
    public override void UseAbility(InputAction.CallbackContext context)
    {
        
    }

}
