using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap gameTilemap;
    UnitSelection _input;
    Camera _camera;
    Entity currentEntity;
    Entity prevEntity;

    private void Awake()
    {
        _input = new UnitSelection();
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
        _input.Input.Select.performed += _ => SelectUnit();
    }

    void SelectUnit()
    {
        prevEntity = currentEntity;
        Vector2 mousePos = _input.Input.Pos.ReadValue<Vector2>();
        mousePos = _camera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        bool hitFound = hit.collider != null;
        bool isGoblin() => currentEntity is BaseGoblin;
        bool isTargetable() => prevEntity != null && ((BaseGoblin)prevEntity).entitiesInRange.Contains(currentEntity);
        bool canDeselect() => prevEntity != null && prevEntity != currentEntity && isGoblin();

        if(!hitFound) return;
        currentEntity = hit.collider.gameObject.GetComponent<BaseGoblin>();

        if(isGoblin() && isTargetable()) return;

        if(canDeselect()) ((BaseGoblin)prevEntity).actionManager.Deselect();
        
        if(!isGoblin()) return;
        ((BaseGoblin)currentEntity).OnClick();
    }
}