using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class BaseGoblin : MonoBehaviour, IClickable
{

    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private int Range;
    [SerializeField]
    public int MovementRange;
    [SerializeField]
    private int Cooldown;
    public EntityMovementController movementController;

    public bool isSelected = false;

    void Start()
    {
        movementController = GetComponent<EntityMovementController>();
    }

    void Update()
    {   
    }

    public void OnClick() {
        PhotonView photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            isSelected = isSelected ? movementController.Deselect() : movementController.onSelect();
        }
        else {
            Debug.LogError($"[OnClick] Ownership: {PhotonNetwork.LocalPlayer.ActorNumber} does not own this entity ; Belongs to {photonView.Owner.ActorNumber}");
            // Insert Function Attack Here 
        }

    }

    public Vector3 getCurrentPos()
    {
        Vector3 pos = transform.position;
        pos.y -= 0.16f; 
        return pos;
    }

}