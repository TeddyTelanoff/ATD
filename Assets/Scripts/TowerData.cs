using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower", fileName = "Tower")]
public class TowerData: ScriptableObject
{
	public GameObject model;
	public Sprite sprite;
	public int price;

	public DartProperty props;
	public float reload;
	public float kb;
	public int dps;
	public int damage;
	public int pierce;
	public int range;

	public Upgrade[] upgradesPath1 = new Upgrade[5];
	public Upgrade[] upgradesPath2 = new Upgrade[5];
	public Upgrade[] upgradesPath3 = new Upgrade[5];
}
