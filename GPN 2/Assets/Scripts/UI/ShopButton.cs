using UnityEngine;

public class ShopButton : UIElement, IClickable
{
    private bool _enabled;

    private void Start() {
        _enabled = GameObject.FindWithTag("ShopPanel").GetComponent<SpriteRenderer>().enabled;
    }

    public void OnClick(GameObject prevSelection = null)
    {
        _enabled = !_enabled;
        GameObject.FindWithTag("ShopPanel").GetComponent<SpriteRenderer>().enabled = _enabled;
        GameObject.FindWithTag("ShopManager").GetComponent<ShopManager>().RenderItemsForSale(_enabled);
    }
}
