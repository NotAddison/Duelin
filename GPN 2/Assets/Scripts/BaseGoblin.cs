using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BaseGoblin : Entity, IClickable
{
    public int Damage;
    public int Health;
    public int Range;
    public int MovementRange;
    public List<Entity> entitiesInRange;
    public EntityActionManager actionManager;
    public bool isSelected;
    public bool isMovementBlocked;

    void Start()
    {
        actionManager = GetComponent<EntityActionManager>();
        entitiesInRange = new List<Entity>();
        isSelected = false;
        isMovementBlocked = false;
    }

    public void OnClick() {
        PhotonView photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            isSelected = isSelected ? actionManager.Deselect() : actionManager.Select();
        }
        else {
            Debug.LogError($"[OnClick] Ownership: {PhotonNetwork.LocalPlayer.ActorNumber} does not own this entity ; Belongs to {photonView.Owner.ActorNumber}");
        }
    }

    public Vector3 getCurrentPos()
    {
        Vector3 pos = transform.position;
        pos.y -= 0.16f; 
        return pos;
    }

    public virtual void UsePassive() {}

    public virtual void UseAbility() {}

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        Health -= attackingEntity.Damage;
        if (Health <= 0) OnDeath(attackingEntity);
    }

    public override void OnDeath(BaseGoblin attackingEntity, Vector3? targetPos = null)
    {
        Destroy(this.gameObject);
        if (attackingEntity.Range > 1) attackingEntity.isMovementBlocked = true;
    }
}