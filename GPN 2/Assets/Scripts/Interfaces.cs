using UnityEngine;

public interface IClickable
{
        void OnClick(GameObject prevSelection = null);
}

public interface IBuyable
{
    int Cost { get; set; }
}