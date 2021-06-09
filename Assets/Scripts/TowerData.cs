using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerData
{
	[NonSerialized]
	public string file;
	[NonSerialized]
	public Sprite icon;
	[NonSerialized]
	public GameObject mesh;

	public string name;
	public int price;

	public DartProperty props;
	public float stick;
	public float explosion;
	public float dartSpeed;
	public float reload;
	public float range;
	public float kb;
	public int dps;
	public int blast;
	public int damage;
	public int pierce;

	public Upgrade[] path1;
	public Upgrade[] path2;
	public Upgrade[] path3;
}
