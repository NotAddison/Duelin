using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BaseGoblin : MonoBehaviour, IClickable
{

    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private int Range;
    [SerializeField]
    public int MovementRange;
    public EntityMovementController movementController;

    public bool isSelected = false;

    void Start()
    {
        movementController = GetComponent<EntityMovementController>();
    }

    void Update()
    {   
    }

    public void OnClick() {
        isSelected = isSelected ? movementController.Deselect() : movementController.onSelect();
    }

    public Vector3 getCurrentPos()
    {
        Vector3 pos = transform.position;
        pos.y -= 0.16f; 
        return pos;
    }

}