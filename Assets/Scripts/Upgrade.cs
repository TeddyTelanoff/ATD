using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Upgrade
{
	public Sprite sprite;
	public string name;
	public int price;

	public DartProperty props;
	public float reload;
	public float kb;
	public int dps;
	public int damage;
	public int pierce;
	public int range;
}
