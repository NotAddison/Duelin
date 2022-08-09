using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Builder : BaseGoblin
{
    private Tilemap gameTilemap;
    private Tilemap buildHighlightMap;
    private Tilemap wallTilemap;
    private Tile buildHighlight;
    private Tile wall;
    private readonly string GAME_MAP = "Tilemap - GameMap";
    private readonly string WALL_MAP = "Tilemap - Walls";
    private readonly string BUILD_MAP = "Tilemap - Highlight [Build]";
    private readonly string BUILD_HIGHLIGHT = "Levels/Tiles/attack_highlight";
    private readonly string STONE_WALL = "Levels/Tiles/stone_wall";
    protected readonly string MOUSE_POS = "POS";
    public override int Cost() => 2;
    
    new protected void Start() {
        base.Start();
        gameTilemap = GameObject.Find(GAME_MAP).GetComponent<Tilemap>();
        wallTilemap = GameObject.Find(WALL_MAP).GetComponent<Tilemap>();
        buildHighlightMap = GameObject.Find(BUILD_MAP).GetComponent<Tilemap>();
        buildHighlight = Resources.Load<Tile>(BUILD_HIGHLIGHT);
        wall = Resources.Load<Tile>(STONE_WALL);
    }
    public override void UseAbility(InputAction.CallbackContext context)
    {
        displayBuildableTiles();

        Vector2 mousePos = context.action.actionMap.FindAction(MOUSE_POS).ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canBuild(gridPos)) return;
        wallTilemap.SetTile(gridPos, wall);
    }
    private void displayBuildableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canBuild(new Vector3Int(x,y,0))) continue;
                buildHighlightMap.SetTile(new Vector3Int(x,y,0), buildHighlight);
            }
        }
    }

    private void Clear()
    {
        buildHighlightMap.ClearAllTiles();
    }

    private bool canBuild(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(GetCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool inRange = dist <= 1 && dist != 0;
        bool isOccupied = hit.collider != null;
        bool canBuild = inRange && !isOccupied;

        if(canBuild) Debug.Log(hit.collider.name);
        // if(canBuild) AddEntityToRange(hit.collider.gameObject.GetComponent<Wall>());
        return canBuild;
    }
}
