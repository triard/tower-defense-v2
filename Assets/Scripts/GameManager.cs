using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public delegate void CurrencyChanged();
public class GameManager : Singleton<GameManager>
{

	public event CurrencyChanged Changed;
	public TowerBtn ClickedBtn { get; set; }

	public int Currency
	{
		get
		{
			return currency;
		}

		set
		{
			this.currency = value;
			this.currencyTxt.text = value.ToString() + " <color=lime>$</color>";

			OnCurrencyChanged();
		}
	}

	private int currency;

	private int wave = 0;

	private int lives;

	private bool gameOver = false;

	private int health = 15;

	[SerializeField]
	private Text sellText;

	[SerializeField]
	private GameObject upgradePanel;

	[SerializeField]
	private GameObject statsPanel;

	[SerializeField]
	private GameObject gameOverMenu;

	private Tower selectedTower;

	[SerializeField]
	private Text livesTxt;
	
	[SerializeField]
	private Text statText;

	public int Lives
	{
		get
		{
			return lives;
		}

		set
		{
			this.lives = value;
			livesTxt.text = lives.ToString();
			if (lives <=0)
			{
				this.lives = 0;
				GameOver();
			}
		}
	}

	public bool WaveActive
	{
		get
		{
			return activeMonsters.Count > 0;
		}
	}

	[SerializeField]
	private Text waveTxt;

	[SerializeField]
	private Text currencyTxt;

	[SerializeField]
	private GameObject waveBtn;

	public ObjectPool Pool { get; set; }

	private List<Monster> activeMonsters = new List<Monster>();

	private void Awake()
	{
		Pool = GetComponent<ObjectPool>();
	}

	// Use this for initialization
	void Start ()
	{
		Lives = 10;
		Currency = 100;
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleEscape();	
	}

	public void PickTower(TowerBtn towerBtn)
	{
		if (Currency >=towerBtn.Price && !WaveActive)
		{
			this.ClickedBtn = towerBtn;
			Hover.Instance.Activate(towerBtn.Sprite);
		}
	}

	public void BuyTower()
	{
		if (Currency >= ClickedBtn.Price)
		{
			Currency -= ClickedBtn.Price;
			Hover.Instance.Deactivate();
		}
	}

	public void OnCurrencyChanged()
	{
		if (Changed != null)
		{
			Changed();
		}
	}


	public void SelectTower(Tower tower)
	{
		if (selectedTower !=null)
		{
			selectedTower.Select();
		}
		selectedTower = tower;
		selectedTower.Select();

		sellText.text = "+ " + (selectedTower.Price / 2);

		upgradePanel.SetActive(true);
	}

	public void DeselectTower()
	{
		if (selectedTower !=null)
		{
			selectedTower.Select();
		}
		upgradePanel.SetActive(false);

		selectedTower = null;
	}

	private void HandleEscape()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Hover.Instance.Deactivate();
		}
	}

	public void StartWave()
	{
		wave++;

		waveTxt.text = string.Format("Wave: <color=lime>{0}</color>", wave);

		StartCoroutine(SpawnWave());

		waveBtn.SetActive(false);
	}

	private IEnumerator SpawnWave()
	{ 
		LevelManager.Instance.GeneratePath();

		for (int i = 0; i < wave; i++)
		{
			int monsterIndex = Random.Range(0, 4);
			string type = string.Empty;
			switch (monsterIndex)
			{
				case 0:
					type = "BlueMonster";
					break;
				case 1:
					type = "RedMonster";
					break;
				case 2:
					type = "GreenMonster";
					break;
				case 3:
					type = "PurpleMonster";
					break;
			}

			Monster monster = Pool.GetObject(type).GetComponent<Monster>();

			monster.Spawn(health);

			if (wave % 3 == 0)
			{
				health += 5;
			}

			activeMonsters.Add(monster);

			yield return new WaitForSeconds(2.5f);
		}
	}

	public void RemoveMonster(Monster monster)
	{
		activeMonsters.Remove(monster);

		if (!WaveActive && !gameOver)
		{
			waveBtn.SetActive(true);
		}
	}

	public void GameOver()
	{
		if (!gameOver)
		{
			gameOver = true;
			gameOverMenu.SetActive(true);
		}
	}

	public void Restart()
	{
		Time.timeScale = 1;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SellTower()
	{
		if (selectedTower != null)
		{
			Currency += selectedTower.Price / 2;

			selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;

			Destroy(selectedTower.transform.parent.gameObject);

			DeselectTower();
		}
	}

	public void ShowStats()
	{
		statsPanel.SetActive(!statsPanel.activeSelf); 
	}

	public void SetTooltipText(string txt)
	{
		statText.text = txt;
	}
}
