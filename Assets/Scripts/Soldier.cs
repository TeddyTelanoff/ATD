using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier: Tower
{
	public override string Name { get => "Soldier"; }
	public override TowerId Id { get => TowerId.Soldier; }

	protected override void UpgradeInternal(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			switch (path1Tier)
			{
			case Tier.Tier1:
				damage++;
				break;
			case Tier.Tier2:
				dartProps |= DartProperty.Explosive;
				transform.Find("View").localScale += new Vector3(2, 2);
				break;
			case Tier.Tier3:
				damage += 2;
				transform.Find("View").localScale += new Vector3(1, 1);
				break;
			case Tier.Tier4:
				pierce += 2;
				damage++;
				break;
			case Tier.Tier5:
				transform.Find("View").localScale += new Vector3(3, 3);
				pierce += 2;
				damage += 5;
				break;
			}
			break;
		case Path.Path2:
			switch (path2Tier)
			{
			case Tier.Tier1:
				reload -= .2f;
				break;
			case Tier.Tier2:
				reload -= .2f;
				break;
			case Tier.Tier3:
				reload -= .5f;
				pierce++;
				break;
			case Tier.Tier4:
				pierce += 3;
				damage++;
				dartProps |= DartProperty.Flame;
				break;
			case Tier.Tier5:
				transform.Find("View").localScale += new Vector3(5, 5);
				pierce++;
				reload = 0;
				break;
			}
			break;
		case Path.Path3:
			switch (path3Tier)
			{
			case Tier.Tier1:
				pierce += 2;
				break;
			case Tier.Tier2:
				dartProps |= DartProperty.Camo;
				transform.Find("View").localScale += new Vector3(1, 1, 1);
				break;
			case Tier.Tier3:
				pierce += 3;
				dartProps |= DartProperty.Ricochet;
				break;
			case Tier.Tier4:
				transform.Find("View").localScale = new Vector3(99, 99, 1);
				break;
			case Tier.Tier5:
				pierce += 45;
				reload -= .2f;
				break;
			}
			break;
		}
	}

	public override string UpgradeName(Path path, Tier tier)
	{
		return path switch
		{
			Path.Path1 => tier switch
			{
				Tier.Tier0 => "Bigger Barrel",
				Tier.Tier1 => "Cannon",
				Tier.Tier2 => "Missle Launcher",
				Tier.Tier3 => "Boom Box",
				Tier.Tier4 => "Tzar Bomba",
				_ => null,
			},
			Path.Path2 => tier switch
			{
				Tier.Tier0 => "Faster Barrel",
				Tier.Tier1 => "Better Stock",
				Tier.Tier2 => "BB Gun",
				Tier.Tier3 => "Hot Rifle",
				Tier.Tier4 => "Gamer Gun",
				_ => null,
			},
			Path.Path3 => tier switch
			{
				Tier.Tier0 => "Iron Dart",
				Tier.Tier1 => "Night Vision",
				Tier.Tier2 => "Dart Ricochet",
				Tier.Tier3 => "X-Ray Vision",
				Tier.Tier4 => "Ants Finding Society",
				_ => null,
			},

			_ => null,
		};
	}

	public override int UpgradePrice(Path path, Tier tier)
	{
		return path switch
		{
			Path.Path1 => tier switch
			{
				Tier.Tier0 => 50,
				Tier.Tier1 => 500,
				Tier.Tier2 => 900,
				Tier.Tier3 => 1200,
				Tier.Tier4 => 4300,
				_ => 0,
			},
			Path.Path2 => tier switch
			{
				Tier.Tier0 => 100,
				Tier.Tier1 => 120,
				Tier.Tier2 => 800,
				Tier.Tier3 => 1200,
				Tier.Tier4 => 24000,
				_ => 0,
			},
			Path.Path3 => tier switch
			{
				Tier.Tier0 => 120,
				Tier.Tier1 => 350,
				Tier.Tier2 => 1700,
				Tier.Tier3 => 7000,
				Tier.Tier4 => 63000,
				_ => 0,
			},

			_ => 0,
		};
	}
}
