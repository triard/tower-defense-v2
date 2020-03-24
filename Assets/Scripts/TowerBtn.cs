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
}
