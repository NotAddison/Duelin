using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GoldBar : MonoBehaviour
{
    public void RenderAmount()
    {
        Sprite goldAmountSprite = Resources.LoadAll<Sprite>("UI_Atlas").Single(sprite => sprite.name.Equals($"gold_{LocalInventory.getInstance().GetGold()}"));
        SpriteRenderer goldAmountComponent = GetComponent<SpriteRenderer>();
        goldAmountComponent.sprite = goldAmountSprite;
    }
}
