using UnityEngine;

public class ItemCard : UIElement, IClickable
{
    private bool isSelected = false;
    public GameObject item;
    public ItemType type;

    public void OnClick(GameObject prevSelection = null)
    {
        if (TurnManager.getInstance().itemPurchased) return;
        bool canDeselect = prevSelection != null && prevSelection.GetComponent<ItemCard>() != null && prevSelection.GetComponent<ItemCard>() != this;
        bool isSelectionGoblin = prevSelection != null && prevSelection.GetComponent<BaseGoblin>() != null;
        bool isSelectionSpawnTile = prevSelection != null && prevSelection.GetComponent<SpawnManager>() != null;

        // TODO: Migrate deselection & selection to interface

        if (isSelectionGoblin) prevSelection.GetComponent<BaseGoblin>().actionManager.Deselect();
        if (isSelectionSpawnTile) prevSelection.GetComponent<SpawnManager>().Clear();
        if (canDeselect) prevSelection.GetComponent<ItemCard>().Deselect();
        isSelected = isSelected ? Deselect() : Select();
    }

    private bool Select()
    {
        Utility.RenderSprite(transform, $"unit_card_collapsed_selected", "item_card");
        if (type == ItemType.UNIT) SpawnManager.getInstance().DisplaySpawnableTiles();
        else if (type == ItemType.CARD) GameObject.FindWithTag("BuyButton").GetComponent<SpriteRenderer>().enabled = true;


        // TODO: Display selection information

        return true;
    }

    public bool Deselect()
    {
        Utility.RenderSprite(transform, $"unit_card_collapsed", "item_card");
        SpawnManager.getInstance().Clear();
        if (type == ItemType.CARD) GameObject.FindWithTag("BuyButton").GetComponent<SpriteRenderer>().enabled = false;

        
        // TODO: Hide selection information

        isSelected = false;
        return false;
    }

    public void RenderCard(GameObject item, ItemType type)
    {
        Component itemComponent = null;
        this.item = item;
        this.type = type;
        itemComponent = type == ItemType.UNIT ? item.transform.Find("entity").GetComponent<BaseGoblin>() : item.transform.Find("card_icon").GetComponent<SpriteRenderer>();
        string spriteName = itemComponent.gameObject.GetComponent<SpriteRenderer>().sprite.name;
        Utility.RenderSprite(transform, type == ItemType.UNIT ? spriteName : $"{spriteName}_minified", "item", "Atlas");
    }

    public enum ItemType
    {
        UNIT,
        CARD
    }
}