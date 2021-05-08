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
				Tier.Tier0 => "",
				Tier.Tier1 => "",
				Tier.Tier2 => "",
				Tier.Tier3 => "",
				Tier.Tier4 => "",
				_ => null,
			},
			Path.Path2 => tier switch
			{
				Tier.Tier0 => "l",
				Tier.Tier1 => "",
				Tier.Tier2 => "",
				Tier.Tier3 => "",
				Tier.Tier4 => "",
				_ => null,
			},
			Path.Path3 => tier switch
			{
				Tier.Tier0 => "",
				Tier.Tier1 => "",
				Tier.Tier2 => "",
				Tier.Tier3 => "",
				Tier.Tier4 => "",
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
