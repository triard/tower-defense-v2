using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : Tower
{

    [SerializeField]
    private float slowingFactor;

    public float SlowingFactor
    {
        get { return slowingFactor; }
    }

    private void Start()
    {
        ElementType = Element.FROST;

        Upgrades = new TowerUpgrade[]
    {
            new TowerUpgrade(2,1,1,2,10 ),
            new TowerUpgrade(2,1,1,2,20 )
    };
    }

    public override Debuff GetDebuff()
    {
        return new FrostDebuff(SlowingFactor,DebuffDuration,Target);
    }
}
