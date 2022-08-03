using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActionManager : MonoBehaviour
{
    private BaseGoblin entity;
    private EntityActions _actions;
    public EntityMovementController _movementController;
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
        _attackController = EntityAttackController.Create(gameObject, entity, this);
        _abilityController = EntityAbilityController.Create(gameObject, entity, this);
    }

    public bool Select()
    {
        _movementController.clicks = 0;

        _attackController.displayAttackableTiles();
        _actions.Main.Click.performed += _attackController.HandleAttack;
        _movementController.displayMovableTiles();
        _actions.Main.Click.performed += _movementController.HandleMovement;
        _actions.Main.Ability.performed += _abilityController.HandleAbility;

        return true;
    }

    public bool Deselect()
    {
        Debug.Log("Deselect");
        _attackController.Clear();
        _movementController.Clear();
        _actions.Main.Click.performed -= _attackController.HandleAttack;
        _actions.Main.Click.performed -= _movementController.HandleMovement;
        _actions.Main.Ability.performed -= _abilityController.HandleAbility;

        entity.isSelected = false;

        return false;
    }
}
