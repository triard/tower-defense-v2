﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{
    [SerializeField]
    private float tickTime;
    [SerializeField]
    private PosionSplash splashPrefabs;
    [SerializeField]
    private int splashDamage;

    public int SplashDamage
    {
        get { return splashDamage; }
    }
    public float TickTime
    {
        get { return tickTime; }
    }

    private void Start() => ElementType = Element.POISON;

    public override Debuff GetDebuff()
    {
        return new PoisonDebuff(splashDamage, tickTime,splashPrefabs,DebuffDuration,Target);    
    }
}
