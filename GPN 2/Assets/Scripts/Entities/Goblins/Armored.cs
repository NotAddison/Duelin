using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Armored : Tank
{
    public override int Cost() => 2;
    public override void UseAbility(InputAction.CallbackContext context)
    {

    }

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        base.OnDamage(attackingEntity, targetPos);
        Debug.Log(attackingEntity.Damage + " damage armored ");
        if (attackingEntity.Damage > 0)
        {
            // Lower attacker's damage by 1 when attacked
            attackingEntity.Damage -= 1;
        }
    }
}
