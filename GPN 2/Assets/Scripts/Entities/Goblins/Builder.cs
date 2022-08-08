using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Builder : BaseGoblin
{
    private Tilemap gameTilemap;
    private Tilemap tauntHighlightMap;   
    private Tile tauntHighlight;
    private readonly string GAME_MAP = "Tilemap - GameMap";
    private readonly string WALL_MAP = "Tilemap - Walls";
    private readonly string BUILD_MAP = "Tilemap - Highlight [Build]";
    private readonly string BUILD_HIGHLIGHT = "Levels/Tiles/build_highlight";
    public override int Cost() => 2;
    public override void UseAbility(InputAction.CallbackContext context)
    {
        // Vector3Int wallPos = wall.WorldToCell((Vector3) targetPos);
        // wall.SetTile(new Vector3Int(wallPos.x, wallPos.y, 2), null);
        // // wall map set tile then wallq

    }
}
