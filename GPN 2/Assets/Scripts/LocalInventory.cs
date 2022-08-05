using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInventory
{
    private int Currency = 20;
    private List<string> Entities = new List<string>();
    private List<GameObject> Cards = new List<GameObject>();

    void Awake(){
        // [TO-DO]: Load Inventory from Database
        RandomizeEntities();

        // Setting Up Entities (Add to player entity list when purchased) [For 1st round]
        // Entities.Add(GameObject.Find("Prefabs/rogue"));
        // Entities.Add(GameObject.Find("Prefabs/archer"));
        // Entities.Add(GameObject.Find("Prefabs/scout"));
    }

    public List<string> RandomizeEntities(){ // Temporary function, to be removed later (after shop is implemented)
        List<string> AllEntities = GetAllEntities();
        var rand = new System.Random();
        for (int i = 0; i < 3; i++)
        {
            int num = rand.Next(AllEntities.Count);
            Entities.Add(AllEntities[num]);
        }
        return AllEntities;
    }

    public List<string> GetAllEntities(){
        // Get all possible entities from prefab
        List<string> AllEntities = new List<string>();
        AllEntities.Add("Prefabs/rogue");
        AllEntities.Add("Prefabs/archer");
        AllEntities.Add("Prefabs/scout");
        return AllEntities;
    }





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
    public int GetCurrency(){
        return Currency;
    }

    ///<summary>
    ///Adds currency to the player's inventory (e.g: When farm captured / when enemy killed)
    ///</summary>
    public void AddCurrency(int amount){
        Currency += amount;
    }

    ///<summary>
    ///Remove currency to the player's inventory (e.g: When Card/Entities purchased from shop)
    ///</summary>
    public void RemoveCurrency(int amount){
        Currency -= amount;
    }

}
