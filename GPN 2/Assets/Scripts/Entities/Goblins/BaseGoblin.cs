using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class BaseGoblin : Entity, IClickable, IBuyable
{
    #region Properties
    public int Damage;
    public int Health;
    public int Range;
    public int MovementRange;
    public float Cooldown;
    public virtual int Cost() => 1;

    public EntityActionManager actionManager;
    public GameObject unit_card;
    public bool isSelected;
    public bool isMovementBlocked;
    public OCCUPATION_STATE occupationState;

    private List<Entity> entitiesInRange;
    public PhotonView photonView;
    private Transform parent;
    private static int entityIndex = 0;
    #endregion

    #region Constructor
    void Start()
    {
        actionManager = GetComponent<EntityActionManager>();
        photonView = GetComponent<PhotonView>();
        parent = transform.parent;

        entitiesInRange = new List<Entity>();
        isSelected = false;
        isMovementBlocked = false;
        occupationState = OCCUPATION_STATE.FREE;

        RenderHealth(parent);

        if (!photonView.IsMine) return;
        Vector3 cardPosition = new Vector3(-2.06f, (0.89f - (0.28f * entityIndex)), 0f);
        unit_card = Instantiate(Resources.Load<GameObject>("Prefabs/UI/unit_card"), cardPosition, Quaternion.identity);
        LocalInventory.getInstance().UpdateEntityListItem(parent.gameObject, entityIndex);
        entityIndex += 1;
        unit_card.GetComponent<UnitCard>().RenderCard(this);
    }
    #endregion

    #region Virtual Methods
    public virtual void UsePassive() { }
    public virtual void UseAbility() { }
    #endregion

    #region Event Handlers
    public void OnClick(GameObject prevSelection = null)
    {
        if (!photonView.IsMine) return;
        Entity prevEntity = prevSelection?.GetComponent<Entity>();
        bool isEntityGoblin = prevEntity is BaseGoblin;
        BaseGoblin prevGoblin = isEntityGoblin ? (BaseGoblin) prevEntity : null;

        bool canDeselect = isEntityGoblin && prevEntity != this;
        bool isTargetable = isEntityGoblin && prevGoblin.GetEntitiesInRange().Contains(this);

        if (isTargetable) return;
        if (canDeselect) prevGoblin.actionManager.Deselect();

        isSelected = isSelected ? actionManager.Deselect() : actionManager.Select();
    }

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        Health -= attackingEntity.Damage;
        if (Health <= 0)
        {
            OnDeath(attackingEntity);
            return;
        }
        RenderHealth(parent);
        unit_card.GetComponent<UnitCard>().RenderCard(this);
    }

    public override void OnDeath(BaseGoblin attackingEntity, Vector3? targetPos = null)
    {
        Destroy(parent.gameObject);
        if (!photonView.IsMine) return;
        LocalInventory.getInstance().DestroyEntity(parent.gameObject);
        Debug.LogError(parent.gameObject.name);
        Destroy(unit_card);
        entityIndex -= 1;
        if (attackingEntity.Range > 1) attackingEntity.isMovementBlocked = true;
    }
    #endregion

    #region Public Methods
    public void ClearEntitiesInRange() => entitiesInRange.Clear();
    public List<Entity> GetEntitiesInRange() => entitiesInRange;
    public Vector3 GetCurrentPos() => new Vector3(transform.position.x, transform.position.y - 0.16f, transform.position.z);
    public void AddEntityToRange(Entity entity) => entitiesInRange.Add(entity);
    #endregion

    private void RenderHealth(Transform parent)
    {
        Sprite healthSprite = Resources.LoadAll<Sprite>("UI_Atlas").Single(sprite => sprite.name.Equals($"{(photonView.IsMine ? "friendly" : "enemy")}_healthbar_{Health}"));
        SpriteRenderer healthbarComponent = parent.Find("healthbar").gameObject.GetComponent<SpriteRenderer>();
        healthbarComponent.sprite = healthSprite;
    }

    public enum OCCUPATION_STATE
    {
        FREE,
        CAPTURING,
        CAPTURED
    }
}