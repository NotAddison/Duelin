using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BaseGoblin : MonoBehaviour, IClickable
{
    [SerializeField]
    private Tilemap gameTilemap;
    [SerializeField]
    private Tilemap renderMap;
    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private int Range;
    [SerializeField]
    private int MovementRange;
    [SerializeField]
    private Tile highlight;

    private Vector3 destination; 
    private EntityMovement controls;
    private int clicks;
    private List<Vector3Int> availableTiles = new List<Vector3Int>();
    private bool isSelected = false;

    private void Awake()
    {
        controls = new EntityMovement();
    }

    private void OnEnable() 
    {
        controls.Enable();
    }

    private void OnDisable() 
    {
        controls.Disable();
    }
    void Start()
    {
        destination = transform.position;
    }

    void Update()
    {   
        if (transform.position == destination) return;
        Debug.Log("Move");
        transform.position = destination;
        Deselect();
    }

    public void OnClick() {
        isSelected = isSelected ? Deselect() : onSelect();
    }

    private bool onSelect()
    {
        displayMovableTiles();
        clicks = 0;
        controls.Main.Click.performed += HandleMovement;
        return true;
    }

    public bool Deselect()
    {
        Debug.Log("Deselect");
        renderMap.ClearAllTiles();
        controls.Main.Click.performed -= HandleMovement;
        isSelected = false;
        return false;
    }

    private void HandleMovement(InputAction.CallbackContext context)
    {
        clicks++;

        if (clicks == 1) return;

        Vector2 mousePos = controls.Main.Pos.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canMove(gridPos)) return;

        destination = gameTilemap.CellToWorld(gridPos);
        destination.y += 0.16f;
    }

    private void displayMovableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canMove(new Vector3Int(x,y,0))) continue;
                renderMap.SetTile(new Vector3Int(x,y,0), highlight);
            }
        }
    }

    private Vector3 getCurrentPos()
    {
        Vector3 pos = transform.position;
        pos.y -= 0.16f; 
        return pos;
    }

    private bool canMove(Vector3Int targetPos)
    {  
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(getCurrentPos()), targetPos));
        bool hasTile = gameTilemap.HasTile(targetPos);
        bool inRange = dist <= MovementRange;

        return hasTile && inRange;
    }
}