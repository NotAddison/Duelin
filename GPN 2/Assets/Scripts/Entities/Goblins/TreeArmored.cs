using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeArmored : Tank
{
    public override void UseAbility()
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
