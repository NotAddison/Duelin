using UnityEngine;


public class Drummer : BaseGoblin
{
    public override int Cost() => 5;
    
    new void Start()
    {
        base.Start();
        GameObject.FindGameObjectsWithTag("Unit").ForEach(obj => {
            BaseGoblin goblin = obj.GetComponent<BaseGoblin>();
            goblin.Damage -= 1;
        });
    }
}
