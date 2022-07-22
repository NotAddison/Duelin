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
        destination = transform.position;
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
        destination.y += 0.16f;
    }

    private bool canMove(Vector3Int pos)
    {  
        if (!gameTilemap.HasTile(pos)) return false;
        return true;
    }
}

// (x,y,z)

// (5,7,0) : .1, .94, .0
// (4,7,0) : -.06, .86, .0
// (3,7,0) : -.22, .78, .0
// (3,6,0) : -.06, .7, .0 

// (0,+1,0) : -.16, -.08
// (+1,0,0) : +.16, +.08