using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : Tank
{
    public override void UseAbility()
    {
        
    }
    public override void UsePassive()
    {
        base.UsePassive();
        // [TODO] Implement knockback of attacked entity
    }
}
