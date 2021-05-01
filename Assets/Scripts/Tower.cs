using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Path: int
{
	None,
	Path1,
	Path2,
	Path3,
}

public enum Tier: int
{
	Tier0,
	Tier1,
	Tier2,
	Tier3,
	Tier4,
	Tier5,
}

public abstract class Tower: MonoBehaviour
{
	public abstract string Name { get; }

	public int pierce;
	public Path primPath;
	public Path disPath;
	public Tier path1Tier;
	public Tier path2Tier;
	public Tier path3Tier;

	public void Upgrade(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			if (disPath == path)
				break;

			if (primPath != Path.None && primPath != path && path1Tier >= Tier.Tier2)
				break;

			if (path1Tier < Tier.Tier5)
			{
				path1Tier++;
				if (primPath == Path.None)
				{
					if (path1Tier >= Tier.Tier3)
						primPath = path;
				}

				if (path2Tier > Tier.Tier0)
					disPath = Path.Path3;
				if (path3Tier > Tier.Tier0)
					disPath = Path.Path2;

				UpgradeInternal(path);
			}
			break;
		case Path.Path2:
			if (disPath == path)
				break;

			if (primPath != Path.None && primPath != path && path2Tier >= Tier.Tier2)
				break;

			if (path2Tier < Tier.Tier5)
			{
				path2Tier++;
				if (primPath == Path.None)
				{
					if (path2Tier >= Tier.Tier3)
						primPath = path;
				}

				if (path1Tier > Tier.Tier0)
					disPath = Path.Path3;
				if (path3Tier > Tier.Tier0)
					disPath = Path.Path1;

				UpgradeInternal(path);
			}
			break;
		case Path.Path3:
			if (disPath == path)
				break;

			if (primPath != Path.None && primPath != path && path3Tier >= Tier.Tier2)
				break;

			if (path3Tier < Tier.Tier5)
			{
				path3Tier++;
				if (primPath == Path.None)
				{
					if (path3Tier >= Tier.Tier3)
						primPath = path;
				}

				if (path1Tier > Tier.Tier0)
					disPath = Path.Path2;
				if (path2Tier > Tier.Tier0)
					disPath = Path.Path1;

				UpgradeInternal(path);
			}
			break;
		}
	}

	protected abstract void UpgradeInternal(Path path);
	public abstract void Fire(Ant ant);
	public abstract string UpgradeName(Path path, Tier tier);
	public string UpgradeName(Path path)
	{
		return path switch
		{
			Path.Path1 => UpgradeName(path, path1Tier),
			Path.Path2 => UpgradeName(path, path2Tier),
			Path.Path3 => UpgradeName(path, path3Tier),
			_ => null,
		};
	}
}