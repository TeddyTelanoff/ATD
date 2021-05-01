using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
	public static TowerManager Instance { get; private set; }

	public Transform parent;
	public GameObject upgradePanel;
	public GameObject soldierPrefab;

	[Header("No Peeking")]
	public List<Tower> towers;
	public Tower selectedTower;
	public bool spawning;

	private void Start() =>
		Instance = this;

	public void Spawn() =>
		spawning = true;

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return))
			DeSelect();
	}

	public void FixedUpdate()
	{
		if (spawning)
		{
			var tower = Instantiate(soldierPrefab, parent).GetComponent<Tower>();
			towers.Add(tower);
			spawning = false;
		}
	}

	public void Upgrade(int path)
	{
		selectedTower.Upgrade((Path)path);
		UpdatePaths();
	}

	public void Select(Tower tower)
	{
		selectedTower = tower;
		upgradePanel.SetActive(true);
		UpdatePaths();
	}

	public void DeSelect()
	{
		selectedTower = null;
		upgradePanel.SetActive(false);
	}

	public void UpdatePaths()
	{
		upgradePanel.transform.Find("Tower").GetComponent<TMP_Text>().text = selectedTower.Name;
		upgradePanel.transform.Find("Tier").GetComponent<TMP_Text>().text = $"{(int)selectedTower.path1Tier}-{(int)selectedTower.path2Tier}-{(int)selectedTower.path3Tier}";

		var p1Button = upgradePanel.transform.Find("Path (1)");
		var p2Button = upgradePanel.transform.Find("Path (2)");
		var p3Button = upgradePanel.transform.Find("Path (3)");
		switch (selectedTower.disPath)
		{
		case Path.None:
			p1Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path1) ?? "MAXED UPGRADE";
			p2Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path2) ?? "MAXED UPGRADE";
			p3Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path3) ?? "MAXED UPGRADE";

			p1Button.transform.Find("Path Price (1)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path1)}";
			p2Button.transform.Find("Path Price (2)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path2)}";
			p3Button.transform.Find("Path Price (3)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path3)}";
			break;
		case Path.Path1:
			p1Button.GetComponentInChildren<TMP_Text>().text = "LOCKED";
			p2Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path2) ?? "MAXED UPGRADE";
			p3Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path3) ?? "MAXED UPGRADE";

			p1Button.transform.Find("Path Price (1)").GetComponent<TMP_Text>().text = "";
			p2Button.transform.Find("Path Price (2)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path2)}";
			p3Button.transform.Find("Path Price (3)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path3)}";
			break;
		case Path.Path2:
			p1Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path1) ?? "MAXED UPGRADE";
			p2Button.GetComponentInChildren<TMP_Text>().text = "LOCKED";
			p3Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path3) ?? "MAXED UPGRADE";

			p1Button.transform.Find("Path Price (1)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path1)}";
			p2Button.transform.Find("Path Price (2)").GetComponent<TMP_Text>().text = "";
			p3Button.transform.Find("Path Price (3)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path3)}";
			break;
		case Path.Path3:
			p1Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path1) ?? "MAXED UPGRADE";
			p2Button.GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path2) ?? "MAXED UPGRADE";
			p3Button.GetComponentInChildren<TMP_Text>().text = "LOCKED";

			p1Button.transform.Find("Path Price (1)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path1)}";
			p2Button.transform.Find("Path Price (2)").GetComponent<TMP_Text>().text = $"${selectedTower.UpgradePrice(Path.Path2)}";
			p3Button.transform.Find("Path Price (3)").GetComponent<TMP_Text>().text = "";
			break;
		}
	}
}
