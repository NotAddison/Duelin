using UnityEngine;

public class TurnManager
{
    private static TurnManager _instance = null;
    public static TurnManager getInstance() => _instance == null ? _instance = new TurnManager() : _instance;

    public void StartTurn()
    {

    }

    public void EndTurn()
    {
        Debug.Log("Turn ended");
    }
}