using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite : AttackSpell
{
    public override int Cost() => 4;
    protected override void HandleEffect(GameObject target = null)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;
        targetGoblin.OnDeath(null);
    }
}
