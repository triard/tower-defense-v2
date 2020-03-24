using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
	[SerializeField]
	private GameObject towerPrefab;

	[SerializeField]
	private Sprite sprite;

	[SerializeField]
	private int price;

	[SerializeField]
	private Text PriceTxt;

	public GameObject TowerPrefab
	{
		get
		{
			return towerPrefab;
		}
	}

	public Sprite Sprite
	{
		get
		{
			return sprite;
		}
	}

	public int Price
	{
		get
		{
			return price;
		}
	}

	private void Start()
	{
		PriceTxt.text = Price + " <color=lime>$</color>";

		GameManager.Instance.Changed += new CurrencyChanged(PriceCheck);
	}

	private void PriceCheck()
	{
		if (price<=GameManager.Instance.Currency)
		{
			GetComponent<Image>().color = Color.white;
			PriceTxt.color = Color.white;
		}
		else
		{
			GetComponent<Image>().color = Color.grey;
			PriceTxt.color = Color.grey;
		}
	}

	public void ShowInfo(string type)
	{
		string toolTip = string.Empty;

		switch (type)
		{
			case "Fire":
				FireTower fire = TowerPrefab.GetComponentInChildren<FireTower>();
				toolTip = string.Format("<color=#ffa500ff><size=20><b>Fire</b></size></color>\nDamage : {0} \nProc: {1}% \nDebuff duration: {2}sec \nTick time: {3}\nTick Damage: {4}\n Can apply a Dot  to the target", fire.Damage, fire.Proc, fire.DebuffDuration,fire.TickTime,fire.TickDamage);
				break;
			case "Frost":
				FrostTower frost = TowerPrefab.GetComponentInChildren<FrostTower>();
				toolTip = string.Format("<color=#00ffffff><size=20><b>Frost</b></size></color>\nDamage : {0} \nProc: {1}% \nDebuff duration: {2}sec \nSlowing factor: {3} \n Has chance to slow down the target", frost.Damage, frost.Proc, frost.DebuffDuration, frost.SlowingFactor);
				break;
			case "Posion":
				PoisonTower poison = TowerPrefab.GetComponentInChildren<PoisonTower>();
				toolTip = string.Format("<color=#00ff00ff><size=20><b>Poison</b></size></color>\nDamage : {0} \nProc: {1}% \nDebuff duration: {2}sec \nTick time: {3} \nSplash damage: {4}\n  Can apply Dripping poison", poison.Damage, poison.Proc, poison.DebuffDuration,poison.TickTime,poison.SplashDamage);
				break;
			case "Strom":
				StromTower strom = TowerPrefab.GetComponentInChildren<StromTower>();
				toolTip = string.Format("<color=#add8e6ff><size=20><b>Strom</b></size></color>\nDamage : {0} \nProc: {1}% \nDebuff duration: {2}sec \n Has a chance to stun the target", strom.Damage, strom.Proc, strom.DebuffDuration);
				break;

		}

		GameManager.Instance.SetTooltipText(toolTip);
		GameManager.Instance.ShowStats();
	}
}
