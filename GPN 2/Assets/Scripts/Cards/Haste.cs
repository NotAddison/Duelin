using UnityEngine;

public class Haste : Enhancement
{
    new public virtual int Cost() => 2;
    protected override void HandleEffect(GameObject target)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;
        targetGoblin.MovementRange += 1;
    }
}
