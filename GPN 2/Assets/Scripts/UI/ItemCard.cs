using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCard : UIElement, IClickable
{
    public void OnClick(GameObject prevSelection = null)
    {
        
    }

    public void RenderCard(GameObject item, ItemType type)
    {
        Component itemComponent = null;
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