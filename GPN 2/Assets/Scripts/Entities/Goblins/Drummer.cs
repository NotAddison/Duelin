using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class Drummer : BaseGoblin
{
    private Tilemap wallTilemap;
    private readonly string WALL_MAP = "Tilemap - Walls";
    
    new void Start()
    {
        base.Start();
        wallTilemap = GameObject.Find(WALL_MAP).GetComponent<Tilemap>();
        wallTilemap.ClearAllTiles();
    }
    public override int Cost() => 5;
    public override void UseAbility(InputAction.CallbackContext context)
    {
        
    }
    public override void UsePassive()
    {
        
    }
}
