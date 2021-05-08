using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washer: Tower
{
	public override string Name { get => "Washer"; }
	public override TowerId Id { get => TowerId.Washer; }

	protected override void UpgradeInternal(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			switch (path1Tier)
			{
			case Tier.Tier1:
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
				break;
			case Tier.Tier3:
				break;
			case Tier.Tier4:
				break;
			case Tier.Tier5:
				break;
			}
			break;
		case Path.Path3:
			switch (path3Tier)
			{
			case Tier.Tier1:
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
				Tier.Tier0 => 0,
				Tier.Tier1 => 0,
				Tier.Tier2 => 0,
				Tier.Tier3 => 0,
				Tier.Tier4 => 0,
				_ => 0,
			},
			Path.Path2 => tier switch
			{
				Tier.Tier0 => 0,
				Tier.Tier1 => 0,
				Tier.Tier2 => 0,
				Tier.Tier3 => 0,
				Tier.Tier4 => 0,
				_ => 0,
			},
			Path.Path3 => tier switch
			{
				Tier.Tier0 => 0,
				Tier.Tier1 => 0,
				Tier.Tier2 => 0,
				Tier.Tier3 => 0,
				Tier.Tier4 => 0,
				_ => 0,
			},

			_ => 0,
		};
	}
}
