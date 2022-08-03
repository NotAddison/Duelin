using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Photon.Pun;

public class EntityAttackController : EntityController
{
    private Tilemap gameTilemap;
    private Tilemap attackHighlightMap;
    private Tile attackHighlight;
    private BaseGoblin entity;
    private Vector3 destination; 
    private EntityActionManager actionManager;
    private int clicks;

    public static EntityAttackController Create(GameObject parent, BaseGoblin entity, EntityActionManager actionManager)
    {
        EntityAttackController _attackController = parent.AddComponent<EntityAttackController>();
        _attackController.entity = entity;
        _attackController.actionManager = actionManager;
        return _attackController;
    }

    private void Start() {
        gameTilemap = GameObject.Find("Tilemap - GameMap").GetComponent<Tilemap>();
        attackHighlightMap = GameObject.Find("Tilemap - UI [Attack]").GetComponent<Tilemap>();
        attackHighlight = Resources.Load<Tile>("Levels/Tiles/highlight_1");
        destination = entity.transform.position;
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.action.actionMap.FindAction("Pos").ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canAttack(gridPos)) return;
        Debug.Log("Attack");
    }

    public void Clear()
    {
        attackHighlightMap.ClearAllTiles();
    }

    public void displayAttackableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canAttack(new Vector3Int(x,y,0))) continue;
                Debug.Log($"Attackable at {new Vector3Int(x,y,0)}");
                attackHighlightMap.SetTile(new Vector3Int(x,y,0), attackHighlight);
            }
        }
    }

    private bool canAttack(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(entity.getCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool inRange = dist <= entity.Range && dist != 0;
        bool isOccupied = hit.collider != null;

        return inRange && isOccupied;
    }
}
