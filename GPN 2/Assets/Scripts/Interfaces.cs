using UnityEngine;

public interface IClickable
{
        void OnClick(Entity prevEntity = null);
}