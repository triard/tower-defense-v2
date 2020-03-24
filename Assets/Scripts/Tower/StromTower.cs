using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StromTower : Tower
{
    private void Start() => ElementType = Element.STROM;

    public override Debuff GetDebuff()
    {
        return new StromDebuff(Target, DebuffDuration);
    }
}
