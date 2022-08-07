using UnityEngine;

public class ShopButton : UIElement, IClickable
{
    private bool _enabled = false;

    public void OnClick(GameObject prevSelection = null)
    {
        _enabled = !_enabled;
        GameObject.FindWithTag("ShopPanel").GetComponent<SpriteRenderer>().enabled = _enabled;
        GameObject.FindWithTag("ShopManager").GetComponent<ShopManager>().RenderItemsForSale(_enabled);
    }
}
