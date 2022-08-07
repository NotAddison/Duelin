using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private List<GameObject> AllUnits;
    private List<GameObject> AllCards;
    private List<GameObject> UnitsForSale;
    private List<GameObject> CardsForSale;
    private System.Random random;

    private void Start() {
        AllUnits = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Units"));
        // AllCards = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Cards"));

        UnitsForSale = new List<GameObject>();
        CardsForSale = new List<GameObject>();

        random = new System.Random();

        for(int i = 0; i < 8; i++) generateUnit();
        // for(int i = 0; i < 3; i++) generateCard();
    }

    private void generateUnit() => UnitsForSale.Add(AllUnits[random.Next(AllUnits.Count)]);
    private void generateCard() => CardsForSale.Add(AllCards[random.Next(AllCards.Count)]);

    public GameObject Purchase(GameObject selectedItem)
    {
        int MAX_UNITS = 5;
        int MAX_CARDS = 4;

        int itemCost = ((IBuyable)(selectedItem.GetComponents<Component>().Single(component => component is IBuyable))).Cost;

        // TODO: UI Hint
        if (itemCost > LocalInventory.getInstance().GetGold()) return null;
        if(UnitsForSale.Contains(selectedItem))
        {
            if (LocalInventory.getInstance().GetEntityListSize() >= MAX_UNITS) return null;
            UnitsForSale.Remove(selectedItem);
            generateUnit();
        }
        if(CardsForSale.Contains(selectedItem))
        {
            if (LocalInventory.getInstance().GetCardListSize() >= MAX_CARDS) return null;
            CardsForSale.Remove(selectedItem);
            generateCard();
        }

        LocalInventory.getInstance().RemoveGold(itemCost);

        return selectedItem;
    }

    public void RenderItemsForSale(bool _enabled)
    {
        if (!_enabled)
        {
            GameObject.FindGameObjectsWithTag("ShopItem").ForEach(item => Destroy(item));
            return;
        }
        
        for (int i = 0; i < 8; i++)
        {
            // GameObject itemForSale = i >= 5 ? CardsForSale[i] : UnitsForSale[i];
            GameObject itemForSale = UnitsForSale[i];
            Vector3 displayPos = new Vector3(1.74f + (i % 2 * 0.31f), -0.06f + ((float) Math.Floor((double)(i / 2)) * -0.28f), 0f);
            GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/UI/item_card");
            GameObject itemPrefabInstance = Instantiate(itemPrefab, displayPos, Quaternion.identity);

            // TODO: Migrate to own object
            string spriteName = itemForSale.transform.Find("entity").GetComponent<BaseGoblin>().gameObject.GetComponent<SpriteRenderer>().sprite.name;
            ExtensionMethods.RenderSprite(itemPrefabInstance.transform, spriteName, "item", "Atlas");
        }
    }
}