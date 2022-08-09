using UnityEngine;

public class Paralyze : AttackSpell
{
    public override int Cost() => 3;
    protected override void HandleEffect(GameObject target = null)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;
        targetGoblin.AddStatus(BaseGoblin.STATUS.PARALYZED, 2);
    }

}
