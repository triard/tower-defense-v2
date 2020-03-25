using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { STROM, FIRE,FROST, POISON, NONE}

public abstract class Tower : MonoBehaviour
{
	[SerializeField]
	private string projectileType;

	[SerializeField]
	private float projectileSpeed;

	public float ProjectileSpeed
	{
		get { return projectileSpeed; }
	}

	private Animator myAnimator;

	[SerializeField]
	private int damage;

	[SerializeField]
	private float debuffDuration;

	[SerializeField]
	private float proc;

	public Element ElementType { get; protected set; }

	private Monster target;

	public int Level { get; protected set; }


	public Monster Target
	{
		get { return target; }
	}

	public int Damage
	{
		get
		{
			return damage;
		}

	}

	private bool canAttack = true;

	private float attackTimer;

	public int Price { get; set; }
	public float DebuffDuration
	{
		get { return debuffDuration; }
		set
		{
			debuffDuration = value;
		}
	}

	public float Proc
	{
		get { return proc; }
		set
		{
			proc = value;
		}
	}

	[SerializeField]
	private float attackCooldown;

	public TowerUpgrade[] Upgrades { get; protected set; } 

	private SpriteRenderer mySpriteRenderer;

	private Queue<Monster> monsters = new Queue<Monster>();


	public TowerUpgrade NextUpgrade
	{
		get{
			if (Upgrades.Length > Level - 1)
			{
				return Upgrades[Level - 1];
			}

			return null;
		}
	}



	// Use this for initialization
	void Awake ()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myAnimator = transform.parent.GetComponent<Animator>();
		Level = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Attack();
		Debug.Log(target);
	}

	public void Select()
	{
		mySpriteRenderer.enabled = !mySpriteRenderer.enabled;

		GameManager.Instance.UpdateUpgradelTip();
	}

	private void Attack()
	{
		if (!canAttack)
		{
			attackTimer += Time.deltaTime;

			if (attackTimer >= attackCooldown)
			{
				canAttack = true;
				attackTimer = 0;
			}
		}

		if (target == null && monsters.Count > 0 && monsters.Peek().IsActive)
		{
			target = monsters.Dequeue();
		}
		if (target != null && target.IsActive)
		{
			if (canAttack)
			{
				Shoot();
				SoundManajer.Instance.PlaySFX("flame");

				myAnimator.SetTrigger("Attack");

				canAttack = false;
			}
		}


		if (!target == null && target.Alive || !target == null && target.IsActive)
		{
			target = null;
		}
	}


	public virtual string GetStats()
	{

		if (NextUpgrade != null)
		{
			return string.Format("\nLevel: {0}\nDamage: {1}<color=#00ff00ff> +{4}</color>\nProc: {2}% <color=#00ff00ff>+{5}%</color>\nDebuff: {3}sec <color=#00ff00ff>+{6}</color>",Level,damage,proc,DebuffDuration, NextUpgrade.Damage, NextUpgrade.ProChance, NextUpgrade.DebuffDuration);
		}
		return string.Format("\nLevel: {0} \nDamage: {1} \nProc: {2}%\nDebuff: {3}sec", Level, damage, proc, DebuffDuration);
	}

	private void Shoot()
	{
		Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

		projectile.transform.position = transform.position;

		projectile.Initialize(this);
	}

	public virtual void Upgrade()
	{
		GameManager.Instance.Currency -= NextUpgrade.Price;
		Price += NextUpgrade.Price;
		this.damage += NextUpgrade.Damage;
		this.proc += NextUpgrade.ProChance;
		this.debuffDuration += NextUpgrade.DebuffDuration;
		Level++;
		GameManager.Instance.UpdateUpgradelTip();
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Monster")
		{
			monsters.Enqueue(other.GetComponent<Monster>());
		}
	}

	public abstract Debuff GetDebuff();

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Monster")
		{
			target = null;
		}
	}
}
