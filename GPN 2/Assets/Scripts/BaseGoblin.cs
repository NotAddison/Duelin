using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BaseGoblin : MonoBehaviour, IClickable
{

    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage;
    [SerializeField]
    public int Range;
    [SerializeField]
    public int MovementRange;
    [SerializeField]
    private int Cooldown;
    public EntityActionManager actionManager;

    public bool isSelected = false;

    void Start()
    {
        actionManager = GetComponent<EntityActionManager>();
    }

    void Update()
    {   
    }

    public void OnClick() {
        PhotonView photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            isSelected = isSelected ? actionManager.Deselect() : actionManager.Select();
        }
        else {
            Debug.LogError($"[OnClick] Ownership: {PhotonNetwork.LocalPlayer.ActorNumber} does not own this entity ; Belongs to {photonView.Owner.ActorNumber}");
        }

    }

    public Vector3 getCurrentPos()
    {
        Vector3 pos = transform.position;
        pos.y -= 0.16f; 
        return pos;
    }

}