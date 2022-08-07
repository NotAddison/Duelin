using UnityEngine;

public interface IClickable
{
    void OnClick(GameObject prevSelection = null);
}

public interface IHoverable
{
    void OnHover();
}

public interface IBuyable
{
    int Cost();
}