using UnityEngine;

public class EntityActionManager : MonoBehaviour
{
    private BaseGoblin entity;
    private EntityActions _actions;
    private EntityMovementController _movementController;
    private EntityAttackController _attackController;
    private EntityAbilityController _abilityController;

    private void Awake() {
        _actions = new EntityActions();
    }

    private void OnEnable() {
        _actions.Enable();
    }

    private void OnDisable() {
        _actions.Disable();
    }

    void Start()
    {
        entity = GetComponent<BaseGoblin>();
        _movementController = EntityMovementController.Create(gameObject, entity, this);
        _attackController   = EntityAttackController.Create(gameObject, entity, this);
        _abilityController  = EntityAbilityController.Create(gameObject, entity, this);
    }

    public bool Select()
    {
        EntityController.clicks = 0;
        entity.isMovementBlocked = false;

        _movementController.displayMovableTiles();
        _attackController.displayAttackableTiles();

        _actions.Main.Click.performed   += _attackController.HandleAttack;
        _actions.Main.Click.performed   += _movementController.HandleMovement;
        _actions.Main.Ability.performed += _abilityController.HandleAbility;

        return true;
    }

    public bool Deselect()
    {
        _attackController.Clear();
        _movementController.Clear();
        entity.entitiesInRange.Clear();
        
        _actions.Main.Click.performed   -= _attackController.HandleAttack;
        _actions.Main.Click.performed   -= _movementController.HandleMovement;
        _actions.Main.Ability.performed -= _abilityController.HandleAbility;

        entity.isSelected = false;

        return false;
    }
}