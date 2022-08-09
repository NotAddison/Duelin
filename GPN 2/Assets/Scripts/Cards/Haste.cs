using UnityEngine;

public class Haste : Enhancement
{
    new public virtual int Cost() => 2;
    protected override void HandleEffect(GameObject target)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        targetGoblin.MovementRange += 1;
    }
}
