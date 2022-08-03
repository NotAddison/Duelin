using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marauder : BaseGoblin
{
    public string code;
    public string skillName;
    public string description;
    public float cooldown;

    public void OnAbilityClick(PointerEventData eventData)
    {
        Debug.Log("Marauder Clicked");
        if (eventData.button == PointerEventData.InputButton.Right){
            Debug.Log("Right Clicked");
            if (cooldown == 0){
                // Cast Skill (Buff friendly units by +1/+1)
                cooldown +=3
            }
        }
    }
}