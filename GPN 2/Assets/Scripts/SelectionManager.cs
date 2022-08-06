using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager : MonoBehaviour
{
    UnitSelection _input;
    UnitSelection.InputActions _inputAction;
    Camera _camera;

    GameObject currentSelection;
    GameObject prevSelection;

    private void Awake()
    {
        _input = new UnitSelection();
        _inputAction = _input.Input;
        _camera = Camera.main;
    }

    private void OnEnable() 
    {
        _input.Enable();
    }

    private void OnDisable() 
    {
        _input.Disable();
    }

    void Start()
    {
        _inputAction.Select.performed += _ => Select();
    }

    void Select()
    {
        prevSelection = currentSelection;
        Vector2 mousePos = _inputAction.Pos.ReadValue<Vector2>();
        mousePos = _camera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        bool hitFound = hit.collider != null;
        bool isClickable() => currentSelection.GetComponents<Component>().Any(component => component is IClickable);

        if(!hitFound) return;
        currentSelection = hit.collider.gameObject;

        Debug.Log($"Clicked on {currentSelection.name}");
        
        if(!isClickable()) return;
        if (!TurnManager.getInstance().CheckTurn()) return;
        ((IClickable)currentSelection.GetComponents<Component>().Single(component => component is IClickable)).OnClick(prevSelection);
    }
}