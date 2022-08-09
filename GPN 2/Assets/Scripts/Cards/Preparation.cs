using UnityEngine;

public class Preparation : Enhancement
{
    new public virtual int Cost() => 1;
    protected override void HandleEffect(GameObject target)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;

        targetGoblin.Health += 1;
        targetGoblin.InitialHealth += 1;
        targetGoblin.Damage += 1;
    }
}