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
}
