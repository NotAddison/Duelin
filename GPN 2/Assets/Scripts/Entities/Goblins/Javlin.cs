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

    public override void OnDeath(BaseGoblin attackingEntity, Vector3? targetPos = null)
    {
        base.OnDeath(attackingEntity, targetPos);
        Clear();
    }

    public override void UseAbility(InputAction.CallbackContext context)
    {
        displayAttackableTiles();

        Vector2 mousePos = context.action.actionMap.FindAction(MOUSE_POS).ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);
        
        if(!canAttack(gridPos)) return;
        PhotonView.Get(this).RPC($"AttackEntity", RpcTarget.All, (Vector3) mousePos);

        if(!canMove(gridPos)) return;

        destination = gameTilemap.CellToWorld(gridPos);
        destination.y += 0.16f;

        PhotonView.Get(this).RPC($"MoveEntity", RpcTarget.All, destination);
    }

    private void displayAttackableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canAttack(new Vector3Int(x,y,0))) continue;
                barrelUnitHighlightMap.SetTile(new Vector3Int(x,y,0), barrelUnitHighlight);
            }
        }
    }

    private void Clear()
    {
        barrelUnitHighlightMap.ClearAllTiles();
    }

    [PunRPC]
    private void AttackEntity(Vector3 targetPos)
    {
        Vector3Int gridPos = gameTilemap.WorldToCell(targetPos);
        Vector3 worldPos = gameTilemap.CellToWorld(gridPos);

        GameObject targetEntity = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero).collider.gameObject;
        Entity target = targetEntity.GetComponent<Entity>();

        if (target != null) {
            target.OnDamage(this, targetPos);
            // entity.UsePassive();
        }

        if (SettingsMenu.getInstance() == null) FindObjectOfType<AudioManager>().Play("Bow", 1f);
        else FindObjectOfType<AudioManager>().Play("Bow", SettingsMenu.getInstance().GetSFXVol());

        // TurnManager.getInstance().HandleTurnAction(TurnManager.ACTION.BONUS_ACTION);
        // actionManager.Deselect();
    }

    [PunRPC]
    public void MoveEntity(Vector3 destination){
        if (transform.position == destination) return;
        transform.parent.position = destination;
        if (SettingsMenu.getInstance() == null) FindObjectOfType<AudioManager>().Play("Move", 1f);
        else FindObjectOfType<AudioManager>().Play("Move", SettingsMenu.getInstance().GetSFXVol());
        
        UsePassive();
        DesyncCheck(destination);
        TurnManager.getInstance().HandleTurnAction(TurnManager.ACTION.ACTION);
        actionManager.Deselect();
    }

    public void DesyncCheck(Vector3 destination){
        destination.y -= 0.16f; // Offset for the tilemap

        if (GetCurrentPos() != destination) // If current entity postion is not the same as destination position
        {
            PhotonView.Get(this).RPC("MoveEntity", RpcTarget.All, destination);
        }
    }

    private bool canAttack(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(GetCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool inRange = dist <= 2 && dist != 0;
        bool isOccupied = hit.collider != null;
        bool isSameTeam = isOccupied && (hit.collider.gameObject.GetComponent<PhotonView>()?.IsMine ?? false);
        bool canAttack = inRange && isOccupied && !isSameTeam;

        if(canAttack) Debug.Log(hit.collider.name);
        Debug.LogError("[Javlin] canAttack: " + inRange + " " + isOccupied + " " + isSameTeam + " " + canAttack);
        if(canAttack) AddEntityToRange(hit.collider.gameObject.GetComponent<Entity>());
        return canAttack;
    }
    private bool canMove(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(GetCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool hasTile = gameTilemap.HasTile(targetPos);
        bool inRange = dist <= 2;
        bool isOccupied = hit.collider != null;
        bool canMove = hasTile && inRange && !isOccupied && !isMovementBlocked;
        Debug.LogError("[Javlin] canMove: " + inRange + " " + isOccupied + " " + isMovementBlocked + "" + canMove);
        return hasTile && inRange && !isOccupied && !isMovementBlocked;
    }
}
