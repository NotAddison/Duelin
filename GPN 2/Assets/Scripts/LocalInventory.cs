using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LocalInventory
{
    private int Gold = 5;
    private List<GameObject> Entities = new List<GameObject>();
    private List<GameObject> Cards = new List<GameObject>();
    private Tilemap mineTilemap;
    private static LocalInventory _instance = null;
    private readonly string MINE_MAP = "Tilemap - Mines";
    public static LocalInventory getInstance() => _instance == null ? _instance = new LocalInventory() : _instance;

    LocalInventory()
    {
        mineTilemap = GameObject.Find(MINE_MAP).GetComponent<Tilemap>();
    }

    public List<GameObject> RandomizeEntities(){ // Temporary function, to be removed later (after shop is implemented)
        List<GameObject> AllUnits = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Units"));
        var rand = new System.Random();
        for (int i = 0; i < 3; i++)
        {
            int num = rand.Next(AllUnits.Count);
            Entities.Add(AllUnits[num]);
        }

        return Entities;
    }

    public void UpdateEntityListItem(GameObject entity, int index) 
    {
        Entities[index] = entity;
    }

    public void UpdateGoldAmount(){
        GetGoblins().ForEach(entity => {
            Vector3Int currentCellPos = mineTilemap.WorldToCell(entity.getCurrentPos());
            bool isOnMine = mineTilemap.HasTile(new Vector3Int(currentCellPos.x, currentCellPos.y, 2));
            bool hasCaptured = entity.occupationState == BaseGoblin.OCCUPATION_STATE.CAPTURED;

            if(isOnMine && !hasCaptured) entity.occupationState = (BaseGoblin.OCCUPATION_STATE) ((int) entity.occupationState + 1);
            if(!isOnMine) entity.occupationState = BaseGoblin.OCCUPATION_STATE.FREE;
            if(isOnMine && hasCaptured) AddGold();
        });
    }

    public void DestroyEntity(GameObject entity)
    {
        int entityIndex = GetPositionOfEntity(entity);
        for (int i = entityIndex + 1; i < Entities.Count; i++)
        {
            GameObject targetEntity = Entities[i];
            GameObject targetEntityUnitCard = targetEntity.transform.Find("entity").GetComponent<BaseGoblin>().unit_card;
            Vector3 targetEntityUnitCardPos = targetEntityUnitCard.transform.position;
            targetEntityUnitCard.transform.position = new Vector3(targetEntityUnitCardPos.x, targetEntityUnitCardPos.y += 0.28f, targetEntityUnitCardPos.z); 
        }
        Entities.RemoveAt(entityIndex);
    }
    
    public int GetPositionOfEntity(GameObject entity) => Entities.FindIndex(e => e == entity);
    public int GetEntityListSize() => Entities.Count; 
    public List<BaseGoblin> GetGoblins() => Entities.Select(entity => entity.transform.Find("entity").GetComponent<BaseGoblin>()).ToList();
    public BaseGoblin GetGoblin(int index) => Entities[index].transform.Find("entity").GetComponent<BaseGoblin>();

    // --------------- Other Inventory Functions ---------------

    // ==== Cards ====
    ///<summary>
    ///Adds a card to the player's inventory (e.g: when purchased from the shop)
    ///</summary>
    public void AddCard(GameObject card){
        Cards.Add(card);
    }

    ///<summary>
    ///Remove a card to the player's inventory (e.g: when Used)
    ///</summary>
    public void RemoveCard(GameObject card){
        Cards.Remove(card);
    }

    ///<summary>
    ///Gets the player's cards
    ///</summary>
    public List<GameObject> GetCards(){
        return Cards;
    }


    // ==== Currency ====
    public int GetGold(){
        return Gold;
    }

    ///<summary>
    ///Adds currency to the player's inventory (e.g: When farm captured / when enemy killed)
    ///</summary>
    private void AddGold(){
        Gold += 2;
    }

    ///<summary>
    ///Remove currency to the player's inventory (e.g: When Card/Entities purchased from shop)
    ///</summary>
    public void RemoveGold(int amount){
        Gold -= amount;
    }

}
