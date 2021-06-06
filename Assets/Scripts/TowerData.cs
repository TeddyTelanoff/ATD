using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerData
{
	[NonSerialized]
	public Sprite sprite;
	public string image;
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

	public Upgrade[] path1;
	public Upgrade[] path2;
	public Upgrade[] path3;
}
