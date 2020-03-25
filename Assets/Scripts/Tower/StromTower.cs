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
     //   if (NextUpgrade != null)
     //   {
      //      return string.Format("<color=#add8e6ff>{0}</color>{1} \nTick time: {2} <color=#00ff00ff>+{4}</color>\nSplash Damage {3} <color=#00ff00ff>+{5} </color>", "<size=20><b>Poison</b></size>", base.GetStats(), tickTime, SplashDamage, NextUpgrade.TickTime, NextUpgrade.SpecialDamage);
       // }

        return string.Format("<color=#add8e6ff>{0}</color>{1}", "<size=20><b>Strom</b></size>", base.GetStats());
    }

    public override void Upgrade()
    {
        base.GetStats();
        base.Upgrade();
    }
}
