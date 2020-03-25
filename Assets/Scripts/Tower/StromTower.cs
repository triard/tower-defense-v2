using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StromTower : Tower
{
    private void Start()
    {
        ElementType = Element.STROM;

        Upgrades = new TowerUpgrade[]
    {
            new TowerUpgrade(2,2,1,2),
            new TowerUpgrade(5,3,1,2 )
    };
    }

    public override Debuff GetDebuff()
    {
        return new StromDebuff(Target, DebuffDuration);
    }

    public override string GetStats()
    {
        return string.Format("<color=#add8e6ff>{0}</color>{1}", "<size=20><b>Strom</b></size>", base.GetStats());
    }
}
