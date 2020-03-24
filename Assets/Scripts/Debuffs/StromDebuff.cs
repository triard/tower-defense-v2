﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StromDebuff : Debuff
{
    public StromDebuff(Monster target, float duration) : base(target,duration)
    {
        if (target !=null)
        {
            target.Speed = 0;
        }
    }

    public override void Remove()
    {
        if (target != null)
        {
            target.Speed = target.MaxSpeed;
            base.Remove(); 
        }
    }
}
