using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [Header("Scene Objects")]
    [SerializeField] private GameObject Scout;
    [SerializeField] private GameObject Tank;

    [Header("Spawn Position")]
    [SerializeField] private Vector3 SpawnPosition;

    void Start()
    {
        Debug.LogError($"[GameManager]: Spawning Object for Player: {PhotonNetwork.LocalPlayer.ActorNumber}");
        SpawnCharacters();
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.F10)){
    //         Debug.LogError("[GameManager]: Spawning Object for Player: " + PhotonNetwork.LocalPlayer.ActorNumber);
    //         PhotonNetwork.Instantiate(Tank.name, new Vector3(0.19f,0.24f,0), Quaternion.identity);
    //     }
    // }

    public void SpawnCharacters(){
        // [TO-DO]: Get players selected characters ; Spawn characters based on selected characters

        List<List<Vector3>> spawnPositions = InitSpawnPos();
        int NumChar = 1;
        string ChracterName = "";

        // Getting Player Character Loadout Info
        // [TO-DO]: Get Player's Number of Characters & Spawning Characters
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            NumChar = 3;
            ChracterName = Scout.name;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            NumChar = 2;
            ChracterName = Tank.name;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            NumChar = 1;
            ChracterName = Scout.name;
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
        {
            NumChar = 1;
            ChracterName = Tank.name;
        }
        
        // ---- Spawn Characters ----
        for (int i = 0; i < NumChar; i++)
        {
            PhotonNetwork.Instantiate(ChracterName, spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber-1][i], Quaternion.identity);
        }
    }

    private List<List<Vector3>> InitSpawnPos(){
        
        List<Vector3> P1 = new List<Vector3>{
            new Vector3 (0.16f, 0.72f, 0),  // Top Right Right
            new Vector3 (0, 0.64f, 0),      // Top Right Middle
            new Vector3 (-0.16f, 0.72f, 0)  // Top Right Left
        };

        List<Vector3> P2 = new List<Vector3>{
            new Vector3 (-1.12f, 0.24f, 0),  // Top Left Left
            new Vector3 (-0.96f, 0.16f, 0), // Top Left Middle
            new Vector3 (-1.12f, 0.08f, 0)   // Top Left Right
        };

        List<Vector3> P3 = new List<Vector3>{
            new Vector3 (1.12f, 0.08f, 0),  // Bottom Right Left
            new Vector3 (0.96f, 0.16f, 0), // Bottom Right Middle
            new Vector3 (1.12f, 0.24f, 0)   // Bottom Right Right
        };

        List<Vector3> P4 = new List<Vector3>{
            new Vector3 (0.16f, -0.4f, 0),  // Bottom Left Right
            new Vector3 (0, -0.32f, 0),      // Bottom Left Middle
            new Vector3 (-0.16f, -0.4f, 0)  // Bottom Left Left
        };

        List<List<Vector3>> spawnPositions = new List<List<Vector3>>();
        spawnPositions.Add(P1);
        spawnPositions.Add(P2);
        spawnPositions.Add(P3);
        spawnPositions.Add(P4);
        return spawnPositions;
    }
}
