using System;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Enhancement
{
    public override int Cost() => 1;
    protected override void HandleEffect(GameObject target = null)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;
        targetGoblin.AddStatus(BaseGoblin.STATUS.REGENERATE, Int32.MaxValue);
    }

}
