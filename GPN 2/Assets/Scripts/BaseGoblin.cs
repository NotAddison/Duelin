using System.Collections.Generic;
using System.Linq;
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

    private PhotonView photonView;
    private Transform parent;

    void Start()
    {
        actionManager = GetComponent<EntityActionManager>();
        photonView = GetComponent<PhotonView>();
        parent = transform.parent;
        entitiesInRange = new List<Entity>();
        isSelected = false;
        isMovementBlocked = false;

        RenderHealth();
    }

    public void OnClick(Entity prevEntity) {

        if(!photonView.IsMine) return;
        bool isEntityGoblin = BaseGoblin.IsEntityGoblin(prevEntity);
        BaseGoblin prevGoblin = isEntityGoblin ? (BaseGoblin) prevEntity : null;
        bool canDeselect() => isEntityGoblin && prevEntity != this;
        bool isTargetable() => isEntityGoblin && prevGoblin.entitiesInRange.Contains(this);

        if(isTargetable()) return;
        if(canDeselect()) prevGoblin.actionManager.Deselect();

        isSelected = isSelected ? actionManager.Deselect() : actionManager.Select();
    }

    public Vector3 getCurrentPos() => new Vector3(transform.position.x, transform.position.y - 0.16f, transform.position.z);

    public virtual void UsePassive() {}

    public virtual void UseAbility() {}

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        Health -= attackingEntity.Damage;
        if (Health <= 0) {
            OnDeath(attackingEntity);
            return;
        }
        RenderHealth();
    }

    public override void OnDeath(BaseGoblin attackingEntity, Vector3? targetPos = null)
    {
        Destroy(parent.gameObject);
        if (attackingEntity.Range > 1) attackingEntity.isMovementBlocked = true;
    }

    public static bool IsEntityGoblin(Entity entity) => entity is BaseGoblin;

    private void RenderHealth()
    {
        Sprite healthSprite = Resources.LoadAll<Sprite>("UI_Atlas").Single(sprite => sprite.name.Equals($"{(photonView.IsMine ? "friendly" : "enemy")}_healthbar_{Health}"));
        SpriteRenderer healthbarComponent = parent.Find("healthbar").gameObject.GetComponent<SpriteRenderer>();
        healthbarComponent.sprite = healthSprite;
    }
}