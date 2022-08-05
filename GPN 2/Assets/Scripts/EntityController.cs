using UnityEngine;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{
    protected readonly string MOUSE_POS = "Pos";
    protected readonly string GAME_MAP = "Tilemap - GameMap";
    public string ACTION_NAME;
    public static int clicks;

    public virtual void HandleAction(InputAction.CallbackContext context) {}
    public virtual void Clear() {}
}
