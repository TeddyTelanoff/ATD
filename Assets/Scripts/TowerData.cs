using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerData
{
	public GameObject model;
	public Sprite sprite;
	public string name;
	public int price;

	public DartProperty props;
	public float effectLifetime;
	public float reload;
	public float kb;
	public int dps;
	public int damage;
	public int pierce;
	public int range;

	public Upgrade[] upgradesPath1;
	public Upgrade[] upgradesPath2;
	public Upgrade[] upgradesPath3;
}
