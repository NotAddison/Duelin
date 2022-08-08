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
        _abilityController.ACTION_TYPE = ACTION.RIGHT_CLICK;
        return _abilityController;
    }

    public override void HandleAction(InputAction.CallbackContext context)
    {
        Debug.Log($"Right click performed {entity}");
        entity.UseAbility(context);
    }

    public delegate bool renderCondition(Vector3Int targetPos);
}
