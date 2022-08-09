using System;
using UnityEngine;

public class SilentStep : Enhancement
{
    public override int Cost() => 1;
    protected override void HandleEffect(GameObject target = null)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;
        targetGoblin.AddStatus(BaseGoblin.STATUS.SILENT, Int32.MaxValue);
    }
}
