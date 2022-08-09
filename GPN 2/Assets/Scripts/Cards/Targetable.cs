using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Targetable : Card
{
    protected Tilemap gameTilemap;
    private Tilemap targetTilemap;
    private Tile targetHighlight;
    private readonly string GAME_MAP = "Tilemap - GameMap";
    private readonly string TARGET_MAP = "Tilemap - Highlight [Targetables]";
    private readonly string TARGET_HIGHLIGHT = "Levels/Tiles/taunt_highlight";

    new private void Start()
    {
        base.Start();
        gameTilemap = GameObject.Find(GAME_MAP).GetComponent<Tilemap>();
        targetTilemap = GameObject.Find(TARGET_MAP).GetComponent<Tilemap>();
        targetHighlight = Resources.Load<Tile>(TARGET_HIGHLIGHT);
    }

    public override bool Select()
    {
        base.Select();
        DisplayEligibleTargets();
        return true;
    }

    public override bool Deselect()
    {
        base.Deselect();
        CLear();
        return false;
    }

    private void DisplayEligibleTargets()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!isEligible(new Vector3Int(x,y,0))) continue;
                Debug.Log($"Eligible tile at {new Vector3Int(x,y,0)}");
                targetTilemap.SetTile(new Vector3Int(x,y,0), targetHighlight);
            }
        }
    }

    private void CLear() => targetTilemap.ClearAllTiles();

    protected virtual bool isEligible(Vector3Int targetPos) => true;
}
