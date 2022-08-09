using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Photon.Pun;


public class Javlin : BaseGoblin
{
    private Tilemap gameTilemap;
    private Tilemap barrelUnitHighlightMap;   
    private Tile barrelUnitHighlight;
    private Vector3 destination; 
    private readonly string GAME_MAP = "Tilemap - GameMap";
    private readonly string BARREL_MAP = "Tilemap - Highlight [Barrel]";
    private readonly string BARREL_HIGHLIGHT = "Levels/Tiles/attack_highlight";
    protected readonly string MOUSE_POS = "POS";
    public override int Cost() => 3;

    new protected void Start() {
        base.Start();
        gameTilemap = GameObject.Find(GAME_MAP).GetComponent<Tilemap>();
        barrelUnitHighlightMap = GameObject.Find(BARREL_MAP).GetComponent<Tilemap>();
        barrelUnitHighlight = Resources.Load<Tile>(BARREL_HIGHLIGHT);
    }
}
