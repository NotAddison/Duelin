using UnityEngine;
using Photon.Pun;

public class Preparation : Targetable
{
    protected override void HandleEffect(GameObject target)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        targetGoblin.Health += 1;
        targetGoblin.Damage += 1;
    }

    protected override bool isEligible(Vector3Int targetPos)
    {
        Vector3 worldPos = gameTilemap.CellToWorld(targetPos);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y += 0.16f), Vector2.zero);
        bool isOccupied = hit.collider != null && hit.collider.name != "spawn(Clone)";
        bool isSameTeam = isOccupied && (hit.collider.gameObject.GetComponent<PhotonView>()?.IsMine ?? false);

        Debug.Log(isSameTeam);

        return isOccupied && isSameTeam;
    }
}
