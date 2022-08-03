using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : Entity
{
    private Tilemap walls;

    void Start()
    {
        walls = GetComponent<Tilemap>();
    }

    public override void OnDamage(BaseGoblin attackingEntity, Vector3 targetPos)
    {
        Vector3Int wallPos = walls.WorldToCell(targetPos);
        walls.SetTile(new Vector3Int(wallPos.x, wallPos.y, 2), null);
    }
}
