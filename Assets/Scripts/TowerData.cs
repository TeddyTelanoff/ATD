using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower", fileName = "Tower")]
public class TowerData: ScriptableObject
{
	public Sprite sprite;
	public int price;

	public Sprite[] spritesPath1;
	public Sprite[] spritesPath2;
	public Sprite[] spritesPath3;

	public Upgrade[] upgradesPath1;
	public Upgrade[] upgradesPath2;
	public Upgrade[] upgradesPath3;
}
