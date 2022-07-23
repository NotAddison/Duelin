using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityMovementController : MonoBehaviour
{
    [SerializeField]
    private Tilemap gameTilemap;

    // [SerializeField]
    // private Tilemap collTilemap;

    private EntityMovement controls;
    private Vector3 destination; 
    private Vector3 start;
    private BaseGoblin entity;

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

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<BaseGoblin>();
        destination = transform.position;
        start = transform.position;
        start.y -= 0.16f;
        controls.Main.Movement.performed += _ => Move();
    }

    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime);
        transform.position = destination;
    }

    private void Move()
    {
        Vector2 mousePos = controls.Main.Pos.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = gameTilemap.WorldToCell((Vector3) mousePos);
        if(!canMove(gridPos)) return;
        destination = gameTilemap.CellToWorld(gridPos);
        start = destination;
        destination.y += 0.16f;
    }

    private bool canMove(Vector3Int pos)
    {  
        int dist = (int) Vector3.Distance(gameTilemap.WorldToCell(start), pos);
        bool hasTile = gameTilemap.HasTile(pos);
        bool inRange = dist <= entity.MovementRange;

        if(!inRange) Debug.Log("Out of range");

        return hasTile && inRange;
    }
}

// (x,y,z)

// (5,7,0) : .1, .94, .0
// (4,7,0) : -.06, .86, .0
// (3,7,0) : -.22, .78, .0
// (3,6,0) : -.06, .7, .0 

// (0,+1,0) : -.16, -.08
// (+1,0,0) : +.16, +.08