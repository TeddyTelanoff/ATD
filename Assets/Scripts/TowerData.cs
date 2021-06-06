using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TowerData
{
	public Sprite sprite { get; set; }
	public string name { get; set; }
	public int price { get; set; }

	public DartProperty props { get; set; }
	public float effectLifetime { get; set; }
	public float reload { get; set; }
	public float kb { get; set; }
	public int dps { get; set; }
	public int damage { get; set; }
	public int pierce { get; set; }
	public int range { get; set; }

	public Upgrade[] path1 { get; set; }
	public Upgrade[] path2 { get; set; }
	public Upgrade[] path3 { get; set; }
}
