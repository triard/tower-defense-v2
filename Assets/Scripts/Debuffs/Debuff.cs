using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{

    protected Monster target;

    private float duration;
    private float elapSed;

    public Debuff(Monster target, float duration)
    {
        this.target = target;
        this.duration = duration;
    }

    public virtual void Update()
    {
        elapSed += Time.deltaTime;
        if (elapSed >= duration)
        {
            Remove();
        }
    }

    public virtual void Remove()
    {
        if (target!=null)
        {
            target.RemoveDebuff(this);  
        }
    }
}
