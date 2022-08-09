using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class Disguised : BaseGoblin
{
    private Tilemap gameTilemap;
    private Tilemap unitsHighlightMap;
    private Tile unitsHighlight;
    private readonly string GAME_MAP = "Tilemap - GameMap";
    private readonly string UNITS_MAP = "Tilemap - Highlight [Units]";
    private readonly string UNITS_HIGHLIGHT = "Levels/Tiles/attack_highlight";
    protected readonly string MOUSE_POS = "POS";
    public override int Cost() => 4;
    new protected void Start() {
        base.Start();
        gameTilemap = GameObject.Find(GAME_MAP).GetComponent<Tilemap>();
        unitsHighlightMap = GameObject.Find(UNITS_MAP).GetComponent<Tilemap>();
        unitsHighlight = Resources.Load<Tile>(UNITS_HIGHLIGHT);
    }
    public override void UseAbility(InputAction.CallbackContext context)
    {
        displayCopyableTiles();

        Vector2 mousePos = context.action.actionMap.FindAction(MOUSE_POS).ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canCopy(gridPos)) return;
        Debug.LogError("Copy");
           
    }
    private void displayCopyableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canCopy(new Vector3Int(x,y,0))) continue;
                unitsHighlightMap.SetTile(new Vector3Int(x,y,0), unitsHighlight);
            }
        }
    }


    private void Clear()
    {
        unitsHighlightMap.ClearAllTiles();
    }

    private bool canCopy(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool isOccupied = hit.collider != null;
        bool canCopy = isOccupied;

        if(canCopy) Debug.Log(hit.collider.name);
        // if(canBuild) AddEntityToRange(hit.collider.gameObject.GetComponent<Wall>());
        return canCopy;
    }
}
