using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos);
}
