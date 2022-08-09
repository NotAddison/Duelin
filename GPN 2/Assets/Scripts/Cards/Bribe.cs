using UnityEngine;
using Photon.Pun;

public class Bribe : AttackSpell
{
    public override int Cost() => 4;
    protected override void HandleEffect(GameObject target = null)
    {
        BaseGoblin targetGoblin = target.GetComponent<BaseGoblin>();
        if(!isEligible(gameTilemap.WorldToCell(targetGoblin.transform.position))) return;
        targetGoblin.AddStatus(BaseGoblin.STATUS.BRIBED, 2);
        PhotonView.Get(targetGoblin).TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}