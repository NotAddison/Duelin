using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    private Vector3 SpawnPosition;

    void Start()
    {
        Debug.LogError($"[GameManager]: Spawning Object for Player: {PhotonNetwork.LocalPlayer.ActorNumber}");
        SpawnCharacters();
    }

    public void SpawnCharacters(){
        // [TO-DO]: Get players selected characters ; Spawn characters based on selected characters
        List<List<Vector3>> spawnPositions = InitSpawnPos();
        LocalInventory localInventory = new LocalInventory();

        List<string> Entities = localInventory.RandomizeEntities();
        Debug.LogError($"[GameManager]: Spawning {Entities.Count} Entities");
        // ---- Spawn Characters ----
        for (int i = 0; i < Entities.Count; i++)
        {
            Debug.LogError($"[GameManager]: Spawning {Entities[i]} at {spawnPositions[PhotonNetwork.LocalPlayer.ActorNumber - 1][i]}");
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
