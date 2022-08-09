using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tank : BaseGoblin
{
    private Tilemap gameTilemap;
    private Tilemap tauntHighlightMap;   
    private Tile tauntHighlight;
    private readonly string GAME_MAP = "Tilemap - GameMap";
    private readonly string TAUNT_MAP = "Tilemap - Highlight [Taunt]";
    private readonly string TAUNT_HIGHLIGHT = "Levels/Tiles/taunt_highlight";

    new protected void Start() {
        base.Start();
        gameTilemap = GameObject.Find(GAME_MAP).GetComponent<Tilemap>();
        tauntHighlightMap = GameObject.Find(TAUNT_MAP).GetComponent<Tilemap>();
        tauntHighlight = Resources.Load<Tile>(TAUNT_HIGHLIGHT);
    }

    public override void OnDeath(BaseGoblin attackingEntity, Vector3? targetPos = null)
    {
        base.OnDeath(attackingEntity, targetPos);
        Clear();
    }
    public override void UsePassive()
    {
        Clear();
        displayTauntedTiles();
    }

    private void displayTauntedTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!isTaunted(new Vector3Int(x,y,0))) continue;
                tauntHighlightMap.SetTile(new Vector3Int(x,y,0), tauntHighlight);
            }
        }
    }   

    public override void Clear()
    {
        tauntHighlightMap.ClearAllTiles();
    }

    private bool isTaunted(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(GetCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool inRange = dist <= 1 && dist != 0;
        bool hasTile = gameTilemap.HasTile(targetPos);

        return inRange && hasTile;
    }
}