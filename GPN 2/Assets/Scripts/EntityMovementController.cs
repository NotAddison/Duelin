using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Photon.Pun;

public class EntityMovementController : EntityController
{
    private Tilemap gameTilemap;
    private Tilemap movementHighlightMap;   
    private Tile movementHighlight;
    private BaseGoblin entity;
    private Vector3 destination; 
    private EntityActionManager actionManager;
    public int clicks;

    public static EntityMovementController Create(GameObject parent, BaseGoblin entity, EntityActionManager actionManager)
    {
        EntityMovementController _movementController = parent.AddComponent<EntityMovementController>();
        _movementController.entity = entity;
        _movementController.actionManager = actionManager;

        return _movementController;
    }

    private void Start() {
        gameTilemap = GameObject.Find("Tilemap - GameMap").GetComponent<Tilemap>();
        movementHighlightMap = GameObject.Find("Tilemap - UI").GetComponent<Tilemap>();
        movementHighlight = Resources.Load<Tile>("Levels/Tiles/highlight");
        destination = entity.transform.position;
    }

    [PunRPC]
    public void MoveEntity(Vector3 destination){
        if (entity.transform.position == destination) return;
        Debug.Log("[MoveEntity]: Moved Entity to " + destination);
        entity.transform.position = destination;
        DesyncCheck(destination);
        TurnManager.getInstance().EndTurn();
        actionManager.Deselect();
    }

    public void DesyncCheck(Vector3 destination){
        destination.y -= 0.16f; // Offset for the tilemap
        Debug.LogError($"[DesyncCheck] isDesync: {entity.getCurrentPos() != destination}");

        if (entity.getCurrentPos() != destination) // If current entity postion is not the same as destination position
        {
            Debug.LogError("[DesyncCheck] Syncing Entity");
            entity.GetComponent<PhotonView>().RPC("MoveEntity", RpcTarget.All, destination);
        }
    }

    public void Clear()
    {
        movementHighlightMap.ClearAllTiles();
    }

    public void displayMovableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canMove(new Vector3Int(x,y,0))) continue;
                movementHighlightMap.SetTile(new Vector3Int(x,y,0), movementHighlight);
            }
        }
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        clicks++;

        if (clicks <= 1) return;

        Vector2 mousePos = context.action.actionMap.FindAction("Pos").ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canMove(gridPos)) return;

        destination = gameTilemap.CellToWorld(gridPos);
        destination.y += 0.16f;
        Debug.Log("Move");

        // Sync Movement (Calls the MoveEntity Method instead of Using Update(only for local))
        PhotonView.Get(this).RPC($"MoveEntity", RpcTarget.All, destination);
    }

    private bool canMove(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(entity.getCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool hasTile = gameTilemap.HasTile(targetPos);
        bool inRange = dist <= entity.MovementRange;
        bool isOccupied = hit.collider != null;

        return hasTile && inRange && !isOccupied;
    }
}

// (x,y,z)

// (5,7,0) : .1, .94, .0
// (4,7,0) : -.06, .86, .0
// (3,7,0) : -.22, .78, .0
// (3,6,0) : -.06, .7, .0 

// (0,+1,0) : -.16, -.08
// (+1,0,0) : +.16, +.08