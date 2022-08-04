using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : Tank
{
    public override void UseAbility()
    {

    }
    public override void UsePassive()
    {
        base.UsePassive();
        Debug.Log("Armored reduces attacker damage");
        attackingEntity.Damage -=1;
    }
}
