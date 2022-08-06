using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager : MonoBehaviour
{
    UnitSelection _input;
    UnitSelection.InputActions _inputAction;
    Camera _camera;
    // TODO: Generalize selection handler
    Entity currentEntity;
    Entity prevEntity;

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
        prevEntity = currentEntity;
        Vector2 mousePos = _inputAction.Pos.ReadValue<Vector2>();
        mousePos = _camera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        bool hitFound = hit.collider != null;
        bool isClickable() => currentEntity is IClickable;

        if(!hitFound) return;
        currentEntity = hit.collider.gameObject.GetComponent<Entity>();
        
        if(!isClickable()) return;
        ((IClickable)currentEntity).OnClick(prevEntity);
    }
}