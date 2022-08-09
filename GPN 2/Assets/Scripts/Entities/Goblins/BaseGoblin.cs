using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private STATUS status;
    private int statusDuration;

    private List<Entity> entitiesInRange;
    public PhotonView photonView;
    private Transform parent;
    private static int entityIndex = 0;
    #endregion

    #region Constructor
    protected void Start()
    {
        actionManager = GetComponent<EntityActionManager>();
        photonView = GetComponent<PhotonView>();
        parent = transform.parent;

        entitiesInRange = new List<Entity>();
        isSelected = false;
        isMovementBlocked = false;
        occupationState = OCCUPATION_STATE.FREE;
        status = STATUS.NONE;

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
    public virtual void UseAbility(InputAction.CallbackContext context) { }
    #endregion

    #region Event Handlers
    public void OnClick(GameObject prevSelection = null)
    {
        bool isSelectionCard = prevSelection != null && prevSelection.GetComponent<Targetable>() != null;
        if (isSelectionCard) {
            prevSelection.GetComponent<Targetable>().UseEffect(this.gameObject);
            return;
        }

        if (!photonView.IsMine) return;
        Entity prevEntity = prevSelection?.GetComponent<Entity>();
        bool isSelectionGoblin = prevEntity is BaseGoblin;
        BaseGoblin prevGoblin = isSelectionGoblin ? (BaseGoblin) prevEntity : null;

        bool canDeselect = isSelectionGoblin && prevEntity != this;
        bool isTargetable = isSelectionGoblin && prevGoblin.GetEntitiesInRange().Contains(this);

        if (isTargetable) return;
        if (canDeselect) prevGoblin.actionManager.Deselect();

        isSelected = isSelected ? actionManager.Deselect() : actionManager.Select();
    }

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {   
        if (attackingEntity is Assassin) 
        {
            status = STATUS.POISONED;
            statusDuration = 3;
        }
        if (attackingEntity is Barrel) 
        {
            status = STATUS.SLOWED;
            MovementRange -= 1;
            statusDuration = 2;
        }
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
        if (!photonView.IsMine) {
            if (attackingEntity.photonView.IsMine) LocalInventory.getInstance().AddGold(2);
            Destroy(parent.gameObject);
            return;
        }
        LocalInventory.getInstance().DestroyGoblin(parent.gameObject);
        Destroy(unit_card);
        Destroy(parent.gameObject);
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

    public void HandleStatusEffects()
    {
        switch(status)
        {
            case STATUS.POISONED:
                statusDuration -= 1;
                Health -= 1;
                break;
            case STATUS.SLOWED:
                statusDuration -= 1;
                break;
            case STATUS.NONE:
                break;
            default:
                break;
        }
        Debug.Log(status);
        if (statusDuration <= 0)
        {
            if (status == STATUS.SLOWED){
                MovementRange += 1;
            }
            status = STATUS.NONE;
        }

    }

    private enum STATUS
    {
        SLOWED,
        POISONED,
        NONE
    }

    public enum OCCUPATION_STATE
    {
        FREE,
        CAPTURING,
        CAPTURED
    }
}