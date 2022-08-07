using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : Tank
{
    public override int Cost() => 2;
    public override void UseAbility()
    {

    }

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        base.OnDamage(attackingEntity, targetPos);
        if (attackingEntity.Damage > 0)
        {
            // Lower attacker's damage by 1 when attacked
            attackingEntity.Damage -= 1;
        }
    }
}
