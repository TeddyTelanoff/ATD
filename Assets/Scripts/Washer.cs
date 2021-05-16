using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washer: Tower
{
	public override string Name { get => "Washer"; }
	public override TowerId Id { get => TowerId.Washer; }
	public override int Price { get => 350; }

	protected override void UpgradeInternal(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			switch (path1Tier)
			{
			case Tier.Tier1:
				dartProps |= DartProperty.Explosive;
				break;
			case Tier.Tier2:
				break;
			case Tier.Tier3:
				break;
			case Tier.Tier4:
				break;
			case Tier.Tier5:
				break;
			}
			break;
		case Path.Path2:
			switch (path2Tier)
			{
			case Tier.Tier1:
				break;
			case Tier.Tier2:
				dps++;
				break;
			case Tier.Tier3:
				dps++;
				break;
			case Tier.Tier4:
				dps += 8;
				break;
			case Tier.Tier5:
				dps += 90;
				break;
			}
			break;
		case Path.Path3:
			switch (path3Tier)
			{
			case Tier.Tier1:
				pierce++;
				break;
			case Tier.Tier2:
				kb++;
				break;
			case Tier.Tier3:
				kb++;
				pierce += 2;
				dartProps |= DartProperty.Camo;
				dartProps |= DartProperty.Ricochet;
				break;
			case Tier.Tier4:
				kb += 2;
				damage++;
				pierce += 2;
				break;
			case Tier.Tier5:
				kb += 2;
				damage += 2;
				pierce += 2;
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
				Tier.Tier0 => "Water Baloons",
				Tier.Tier1 => "Poodles",
				Tier.Tier2 => "Washing Machine",
				Tier.Tier3 => "Rain Shower",
				Tier.Tier4 => "Shower Storm",
				_ => null,
			},
			Path.Path2 => tier switch
			{
				Tier.Tier0 => "Water Stains",
				Tier.Tier1 => "Dirty Rinse",
				Tier.Tier2 => "Feces",
				Tier.Tier3 => "Acidox",
				Tier.Tier4 => "Ants Terminator",
				_ => null,
			},
			Path.Path3 => tier switch
			{
				Tier.Tier0 => "Bigger Hose",
				Tier.Tier1 => "Splippery Stuff",
				Tier.Tier2 => "Magic Water",
				Tier.Tier3 => "Power Hose",
				Tier.Tier4 => "Super Hose of Endings",
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
				Tier.Tier0 => 300,
				Tier.Tier1 => 550,
				Tier.Tier2 => 800,
				Tier.Tier3 => 3000,
				Tier.Tier4 => 26000,
				_ => 0,
			},
			Path.Path2 => tier switch
			{
				Tier.Tier0 => 150,
				Tier.Tier1 => 250,
				Tier.Tier2 => 1000,
				Tier.Tier3 => 6400,
				Tier.Tier4 => 32000,
				_ => 0,
			},
			Path.Path3 => tier switch
			{
				Tier.Tier0 => 250,
				Tier.Tier1 => 2000,
				Tier.Tier2 => 2700,
				Tier.Tier3 => 5600,
				Tier.Tier4 => 43000,
				_ => 0,
			},

			_ => 0,
		};
	}
}
