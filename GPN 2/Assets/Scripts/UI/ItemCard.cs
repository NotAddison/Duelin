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
        bool isEntityGoblin = prevSelection != null && prevSelection.GetComponent<BaseGoblin>() != null;
        if (isEntityGoblin) prevSelection.GetComponent<BaseGoblin>().actionManager.Deselect();
        if (canDeselect) prevSelection.GetComponent<ItemCard>().Deselect();
        isSelected = isSelected ? Deselect() : Select();
    }

    private bool Select()
    {
        Utility.RenderSprite(transform, $"unit_card_collapsed_selected", "item_card");
        if (type == ItemType.UNIT) SpawnManager.getInstance().DisplaySpawnableTiles();

        // TODO: Display selection information

        return true;
    }

    public bool Deselect()
    {
        Utility.RenderSprite(transform, $"unit_card_collapsed", "item_card");
        SpawnManager.getInstance().Clear();

        // TODO: Hide selection information

        isSelected = false;
        return false;
    }

    public void RenderCard(GameObject item, ItemType type)
    {
        Component itemComponent = null;
        this.item = item;
        this.type = type;
        if (type == ItemType.UNIT) itemComponent = item.transform.Find("entity").GetComponent<BaseGoblin>();
        string spriteName = itemComponent.gameObject.GetComponent<SpriteRenderer>().sprite.name;
        Utility.RenderSprite(transform, spriteName, "item", "Atlas");
    }

    public enum ItemType
    {
        UNIT,
        CARD
    }
}