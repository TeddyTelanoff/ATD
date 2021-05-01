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
		upgradePanel.transform.Find("Tier").GetComponent<TMP_Text>().text = $"{(int)selectedTower.path1Tier}-{(int)selectedTower.path2Tier}-{(int)selectedTower.path3Tier}";

		upgradePanel.transform.Find("Path (1)").GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path1) ?? "LOCKED";
		upgradePanel.transform.Find("Path (2)").GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path2) ?? "LOCKED";
		upgradePanel.transform.Find("Path (3)").GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path3) ?? "LOCKED";
	}

	public void Select(Tower tower)
	{
		selectedTower = tower;
		upgradePanel.SetActive(true);
		upgradePanel.transform.Find("Tower").GetComponent<TMP_Text>().text = tower.Name;
		upgradePanel.transform.Find("Tier").GetComponent<TMP_Text>().text = $"{(int)selectedTower.path1Tier}-{(int)selectedTower.path2Tier}-{(int)selectedTower.path3Tier}";

		upgradePanel.transform.Find("Path (1)").GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path1) ?? "LOCKED";
		upgradePanel.transform.Find("Path (2)").GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path2) ?? "LOCKED";
		upgradePanel.transform.Find("Path (3)").GetComponentInChildren<TMP_Text>().text = selectedTower.UpgradeName(Path.Path3) ?? "LOCKED";

	}

	public void DeSelect()
	{
		selectedTower = null;
		upgradePanel.SetActive(false);
	}
}
