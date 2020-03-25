using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    public float TickTime
    {
        get { return tickTime; }
    }
    public float TickDamage
    {
        get
        {
            return tickDamage;
        }
    }

    private void Start()
    {
        ElementType = Element.FIRE;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,2,.5f,5,-0.1f,1 ),
            new TowerUpgrade(5,3,.5f,5,-0.1f,1 )
        };
    }

    public override Debuff GetDebuff()
    {
        return new FireDebuff(TickDamage,TickTime,DebuffDuration,Target);
    }

    public virtual string GetStats()
    {

        if (NextUpgrade != null)
        {
            return string.Format("<color=#ffa500ff>{0}</color>{1} \nTick time: {2} <color=#00ff00ff>{4}</color>\nTick Damage {3} <color=#00ff00ff>+{5} </color>" ,"<size=20><b>Fire</b></size>", base.GetStats(), tickTime, tickDamage, NextUpgrade.TickTime, NextUpgrade.SpecialDamage);
        }
        return string.Format("<color=#ffa500ff>{0}</color> \nTick time: {2}\nTick damage: {3}","<size=20><b>Fire</b></size>", base.GetStats(), TickTime, TickDamage);
    }


    public override void Upgrade()
    {
        this.tickDamage -= NextUpgrade.SpecialDamage;
        this.tickTime -= NextUpgrade.TickTime;
        base.Upgrade();
    }
}
