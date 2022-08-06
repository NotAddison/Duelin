using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marauder : BaseGoblin
{
    public string code;
    public string skillName;
    public string description;

    public override void UseAbility()
    {
        if (cooldown == 0){
            cooldown += 4;
            // [TODO] Buff player's whole team +1/+1
        }
    }
}