using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;


public class EntityAbilityController : EntityController
{
    private BaseGoblin entity;
    private EntityActionManager actionManager;

    public static EntityAbilityController Create(GameObject parent, BaseGoblin entity, EntityActionManager actionManager)
    {
        EntityAbilityController _abilityController = parent.AddComponent<EntityAbilityController>();
        _abilityController.entity = entity;
        _abilityController.actionManager = actionManager;
        _abilityController.ACTION_NAME = "ABILITY";
        return _abilityController;
    }

    void Start() {

    }

    public override void HandleAction(InputAction.CallbackContext context)
    {
        entity.UseAbility();
    }

    public delegate bool renderCondition(Vector3Int targetPos);
}
