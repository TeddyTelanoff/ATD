using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
	public const string upgradeLocked = "UPGRADE LOCKED", maxUpgrade = "MAXED UPGRADE";
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

		var pt1 = p1Button.Find("Text (TMP)").GetComponent<TMP_Text>();
		var pt2 = p2Button.Find("Text (TMP)").GetComponent<TMP_Text>();
		var pt3 = p3Button.Find("Text (TMP)").GetComponent<TMP_Text>();

		var pp1 = p1Button.Find("Path Price (1)").GetComponent<TMP_Text>();
		var pp2 = p2Button.Find("Path Price (2)").GetComponent<TMP_Text>();
		var pp3 = p3Button.Find("Path Price (3)").GetComponent<TMP_Text>();

		switch (selectedTower.disPath)
		{
		case Path.None:
			pt1.text = selectedTower.UpgradeName(Path.Path1) ?? maxUpgrade;
			pt2.text = selectedTower.UpgradeName(Path.Path2) ?? maxUpgrade;
			pt3.text = selectedTower.UpgradeName(Path.Path3) ?? maxUpgrade;

			pp1.text = $"${selectedTower.UpgradePrice(Path.Path1)}";
			pp2.text = $"${selectedTower.UpgradePrice(Path.Path2)}";
			pp3.text = $"${selectedTower.UpgradePrice(Path.Path3)}";
			break;
		case Path.Path1:
			pt1.text = upgradeLocked;
			pp1.text = "";

			switch (selectedTower.primPath)
			{
			case Path.Path2:
				UpdatePath(Path.Path2, Path.Path3, pt2, pp2, pt3, pp3);
				break;
			case Path.Path3:
				UpdatePath(Path.Path3, Path.Path2, pt3, pp3, pt2, pp2);
				break;

			default:
				UpdatePathNormal(Path.Path2, Path.Path3, pt2, pp2, pt3, pp3);
				break;
			}
			break;
		case Path.Path2:
			pt2.text = upgradeLocked;
			pp2.text = "";

			switch (selectedTower.primPath)
			{
			case Path.Path1:
				UpdatePath(Path.Path1, Path.Path3, pt1, pp1, pt3, pp3);
				break;
			case Path.Path3:
				UpdatePath(Path.Path3, Path.Path1, pt3, pp3, pt1, pp1);
				break;

			default:
				UpdatePathNormal(Path.Path1, Path.Path3, pt1, pp1, pt3, pp3);
				break;
			}
			break;
		case Path.Path3:
			pt3.text = upgradeLocked;
			pp3.text = "";

			switch (selectedTower.primPath)
			{
			case Path.Path1:
				UpdatePath(Path.Path1, Path.Path2, pt1, pp1, pt2, pp2);
				break;
			case Path.Path2:
				UpdatePath(Path.Path2, Path.Path1, pt2, pp2, pt1, pp1);
				break;

			default:
				UpdatePathNormal(Path.Path1, Path.Path2, pt1, pp1, pt2, pp2);
				break;
			}
			break;
		}
	}

	private void UpdatePathNormal(Path a, Path b,
		TMP_Text pta, TMP_Text ppa,
			TMP_Text ptb, TMP_Text ppb)
	{
		pta.text = selectedTower.UpgradeName(a) ?? maxUpgrade;
		ptb.text = selectedTower.UpgradeName(b) ?? maxUpgrade;

		ppa.text = $"${selectedTower.UpgradePrice(a)}";
		ppb.text = $"${selectedTower.UpgradePrice(b)}";
	}

	private void UpdatePath(Path prim, Path sec,
		TMP_Text pt1, TMP_Text pp1,
			TMP_Text pt2, TMP_Text pp2)
	{
		pt1.text = selectedTower.UpgradeName(prim) ?? maxUpgrade;
		pp1.text = $"${selectedTower.UpgradePrice(prim)}";

		if (selectedTower.path3Tier < Tier.Tier2)
		{
			pt2.text = selectedTower.UpgradeName(sec);
			pp2.text = $"${selectedTower.UpgradePrice(sec)}";
			return;
		}

		pt2.text = maxUpgrade;
		pp2.text = "";
	}
}
