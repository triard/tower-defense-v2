using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade
{

    public int Price { get; private set; }

    public int Damage { get; private set; }

    public float DebuffDuration { get; private set; }

    public float ProChance { get; private set; }
    
    public float SlowingFactor { get; private set; }

    public float TickTime { get; private set; }

    public float SpecialDamage { get; private set; }

    public TowerUpgrade(int price, int damage, float debuffDuration, float proChance)
    {
        this.Damage = damage;
        this.DebuffDuration = debuffDuration;
        this.ProChance = proChance;
        this.Price = price;
    }

    public TowerUpgrade(int price, int damage, float debuffDuration, float proChance, float slowingFactor)
    {
        this.Damage = damage;
        this.DebuffDuration = debuffDuration;
        this.ProChance = proChance;
        this.SlowingFactor = slowingFactor;
        this.Price = price;
    }

    public TowerUpgrade(int price, int damage, float debuffDuration, float proChance, float tickTime, float specialdamage)
    {
        this.Damage = damage;
        this.DebuffDuration = debuffDuration;
        this.ProChance = proChance;
        this.TickTime = tickTime;
        this.SpecialDamage = specialdamage;
        this.Price = price;
    }
}
