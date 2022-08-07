using System;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    SelectionActions _input;
    SelectionActions.InputActions _inputAction;
    Camera _camera;

    GameObject currentSelection;
    GameObject prevSelection;

    private void Awake()
    {
        _input = new SelectionActions();
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
        
        if(!isClickable()) return;
        if (!TurnManager.getInstance().CheckTurn()) return;
        ((IClickable)currentSelection.GetComponents<Component>().Single(component => component is IClickable)).OnClick(prevSelection);
    }
}