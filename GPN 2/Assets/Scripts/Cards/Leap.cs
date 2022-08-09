using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leap : Targetable
{
    public override int Cost() => 1;
    protected override void HandleEffect(GameObject target = null)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;
        // TODO
    }
}
