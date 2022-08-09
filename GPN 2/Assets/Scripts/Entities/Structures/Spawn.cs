using UnityEngine;

public class Spawn : Entity
{
    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        OnDeath();
    }

    public override void OnDeath(BaseGoblin attackingEntity = null, Vector3? targetPos = null)
    {
        Debug.Log("You lose");
        GameObject.FindWithTag("WinLoseToast").GetComponent<WinLoseToast>().Render(false);
    }
}