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
    private readonly string MOVEMENT_MAP = "Tilemap - Highlight [Movement]";
    private readonly string MOVEMENT_HIGHLIGHT = "Levels/Tiles/movement_highlight";

    public static EntityMovementController Create(GameObject parent, BaseGoblin entity, EntityActionManager actionManager)
    {
        EntityMovementController _movementController = parent.AddComponent<EntityMovementController>();
        _movementController.entity = entity;
        _movementController.actionManager = actionManager;
        _movementController.ACTION_TYPE = ACTION.LEFT_CLICK;
        return _movementController;
    }

    private void Start() {
        gameTilemap = GameObject.Find(GAME_MAP).GetComponent<Tilemap>();
        movementHighlightMap = GameObject.Find(MOVEMENT_MAP).GetComponent<Tilemap>();
        movementHighlight = Resources.Load<Tile>(MOVEMENT_HIGHLIGHT);
        destination = entity.transform.position;
    }

    public override void HandleAction(InputAction.CallbackContext context)
    {
        clicks++;
        if (clicks <= 2) return;
        if (TurnManager.getInstance().actionTaken) return;

        Vector2 mousePos = context.action.actionMap.FindAction(MOUSE_POS).ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canMove(gridPos)) return;

        destination = gameTilemap.CellToWorld(gridPos);
        destination.y += 0.16f;

        PhotonView.Get(this).RPC($"MoveEntity", RpcTarget.All, destination);
    }

    [PunRPC]
    public void MoveEntity(Vector3 destination){
        if (entity.transform.position == destination) return;
        entity.transform.parent.position = destination;
        if (SettingsMenu.getInstance() == null) FindObjectOfType<AudioManager>().Play("Move", 1f);
        else FindObjectOfType<AudioManager>().Play("Move", SettingsMenu.getInstance().GetSFXVol());
        
        entity.UsePassive();
        DesyncCheck(destination);
        TurnManager.getInstance().HandleTurnAction(TurnManager.ACTION.ACTION);
        actionManager.Deselect();
    }

    public void DesyncCheck(Vector3 destination){
        destination.y -= 0.16f; // Offset for the tilemap

        if (entity.GetCurrentPos() != destination) // If current entity postion is not the same as destination position
        {
            PhotonView.Get(this).RPC("MoveEntity", RpcTarget.All, destination);
        }
    }

    public override void Clear() => movementHighlightMap.ClearAllTiles();

    public void displayMovableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canMove(new Vector3Int(x,y,0))) continue;
                movementHighlightMap.SetTile(new Vector3Int(x,y,0), movementHighlight);
            }
        }
    }

    private bool canMove(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(entity.GetCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool hasTile = gameTilemap.HasTile(targetPos);
        bool inRange = dist <= entity.MovementRange;
        bool isOccupied = hit.collider != null;

        return hasTile && inRange && !isOccupied && !entity.isMovementBlocked;
    }
}