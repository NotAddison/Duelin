using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Photon.Pun;

public class EntityMovementController : MonoBehaviour
{
    [SerializeField]
    private Tilemap gameTilemap;
    [SerializeField]
    private Tilemap renderMap;
    [SerializeField]
    private Tile movementHighlight;
    private BaseGoblin entity;

    private EntityMovement controls;
    private Vector3 destination; 
    private int clicks;

    private void Awake()
    {
        controls = new EntityMovement();
    }

    private void OnEnable() 
    {
        controls.Enable();
    }

    private void OnDisable() 
    {
        controls.Disable();
    }

    void Start()
    {
        entity = GetComponent<BaseGoblin>();
        destination = entity.transform.position;
    }

    void Update()
    {
        
    }

    [PunRPC]
    public void MoveEntity(Vector3 destination){
        if (entity.transform.position == destination) return;
        Debug.LogError("[MoveEntity]: Moved Entity to " + destination);
        entity.transform.position = destination;
        DesyncCheck(destination);
        Deselect();
        TurnManager.getInstance().EndTurn();
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

    public bool onSelect()
    {
        displayMovableTiles();
        clicks = 0;
        controls.Main.Click.performed += HandleMovement;
        return true;
    }

    public bool Deselect()
    {
        Debug.Log("Deselect");
        renderMap.ClearAllTiles();
        controls.Main.Click.performed -= HandleMovement;
        entity.isSelected = false;
        return false;
    }

    private void displayMovableTiles()
    {
        for(int x = gameTilemap.cellBounds.min.x; x <= gameTilemap.cellBounds.max.x; x++) {
            for(int y = gameTilemap.cellBounds.min.y; y <= gameTilemap.cellBounds.max.y; y++) {
                if (!canMove(new Vector3Int(x,y,0))) continue;
                renderMap.SetTile(new Vector3Int(x,y,0), movementHighlight);
            }
        }
    }

    private void HandleMovement(InputAction.CallbackContext context)
    {
        clicks++;

        if (clicks == 1) return;

        Vector2 mousePos = controls.Main.Pos.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);

        if(!canMove(gridPos)) return;

        destination = gameTilemap.CellToWorld(gridPos);
        destination.y += 0.16f;

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