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
	public Sprite sprite;
	[NonSerialized]
	public Mesh mesh;

	public string model;
	public string image;
	public string name;
	public int price;

	public DartProperty props;
	public float effectLifetime;
	public float explosion;
	public float reload;
	public float range;
	public float kb;
	public int dps;
	public int damage;
	public int pierce;

	public Upgrade[] path1;
	public Upgrade[] path2;
	public Upgrade[] path3;
}
