using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
	public Point GridPosition { get; private set; }

	private Color32 fullColor = new Color32(255, 118, 118, 255);
	private Color32 emptyColor = new Color32(96, 255, 90, 255);

	private SpriteRenderer spriteRenderer;

	public bool Debugging { get; set; }

	public bool IsEmpty { get; set; }

	private Tower myTower;

	public bool Walkable { get; set; }

	public Vector2 WorldPosition
	{
		get
		{
			return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
		}
	}

	// Use this for initialization
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
	{
		this.GridPosition = gridPos;
		transform.position = worldPos;
		transform.SetParent(parent);
		LevelManager.Instance.Tiles.Add(gridPos,this);
		IsEmpty = true;
		Walkable = true;
	}

	private void OnMouseOver()
	{
		if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
		{
			if (IsEmpty && !Debugging)
			{
				ColorTile(emptyColor);
			}
			if(!IsEmpty && !Debugging)
			{
				ColorTile(fullColor);
			}
			else if (Input.GetMouseButtonDown(0))
			{
				PlaceTower();
			}
		}
		else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn == null && Input.GetMouseButton(0))
		{
			if (myTower != null)
			{
				GameManager.Instance.SelectTower(myTower);
			}
			else
			{
				GameManager.Instance.DeselectTower();
			}
		}
	}

	private void OnMouseExit()
	{
		if (!Debugging)
		{
			ColorTile(Color.white);
		}
	}

	private void PlaceTower()
	{
		GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
		tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
		tower.transform.SetParent(transform);
		this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
		myTower.Price = GameManager.Instance.ClickedBtn.Price;
		GameManager.Instance.BuyTower();
		ColorTile(Color.white);
		IsEmpty = false;
		Walkable = false;
	}

	private void ColorTile(Color newColor)
	{
		spriteRenderer.color = newColor;
	}
}
