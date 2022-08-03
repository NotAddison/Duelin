using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Photon.Pun;

public class EntityAttackController : EntityController
{
    private Tilemap gameTilemap;
    private Tilemap attackHighlightMap;
    private Tile attackHighlight;
    private BaseGoblin entity;
    private EntityActionManager actionManager;
    private readonly string ATTACK_MAP = "Tilemap - UI [Attack]";
    private readonly string ATTACK_HIGHLIGHT = "Levels/Tiles/highlight_1";

    public static EntityAttackController Create(GameObject parent, BaseGoblin entity, EntityActionManager actionManager)
    {
        EntityAttackController _attackController = parent.AddComponent<EntityAttackController>();
        _attackController.entity = entity;
        _attackController.actionManager = actionManager;
        return _attackController;
    }

    private void Start() {
        gameTilemap = GameObject.Find(GAME_MAP).GetComponent<Tilemap>();
        attackHighlightMap = GameObject.Find(ATTACK_MAP).GetComponent<Tilemap>();
        attackHighlight = Resources.Load<Tile>(ATTACK_HIGHLIGHT);
    }

    [PunRPC]
    private void AttackEntity(Vector3 targetPos)
    {
        Vector3Int gridPos = gameTilemap.WorldToCell(targetPos);
        Vector3 worldPos = gameTilemap.CellToWorld(gridPos);

        GameObject targetEntity = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero).collider.gameObject;
        Entity target = targetEntity.GetComponent<Entity>();

        if (target != null) target.OnDamage(entity, targetPos);

        // TODO: DesyncCheck
        DesyncCheck(targetPos);

        TurnManager.getInstance().EndTurn();
        actionManager.Deselect();
    }

    public void DesyncCheck(Vector3 targetPos){
        // if () entity.GetComponent<PhotonView>().RPC("AttackEntity", RpcTarget.All, targetEntity, targetPos);
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        clicks++;
        if (clicks <= 1) return;
        
        Vector2 mousePos = context.action.actionMap.FindAction(MOUSE_POS).ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canAttack(gridPos)) return;

        PhotonView.Get(this).RPC($"AttackEntity", RpcTarget.All, (Vector3) mousePos);
    }

    public void Clear()
    {
        attackHighlightMap.ClearAllTiles();
    }

    public void displayAttackableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canAttack(new Vector3Int(x,y,0))) continue;
                attackHighlightMap.SetTile(new Vector3Int(x,y,0), attackHighlight);
            }
        }
    }

    private bool canAttack(Vector3Int targetPos)
    {  
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        int dist = (int) Math.Ceiling(Vector3.Distance(gameTilemap.WorldToCell(entity.getCurrentPos()), targetPos));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        
        bool inRange = dist <= entity.Range && dist != 0;
        bool isOccupied = hit.collider != null;
        bool canAttack = inRange && isOccupied;

        if(canAttack) Debug.Log($"Attackable {hit.collider.name} at {targetPos.x}, {targetPos.y}");
        if(canAttack) entity.entitiesInRange.Add(hit.collider.gameObject.GetComponent<Entity>());

        return canAttack;
    }
}