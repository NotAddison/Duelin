using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : Tank
{
    public override void UseAbility()
    {

    }

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        base.OnDamage(attackingEntity, targetPos);
        Health += 1;
    }
}
