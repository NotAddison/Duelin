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
    int Cost { get; set; }

    int getCost() => Cost;
    void setCost(int cost) => Cost = cost;
}