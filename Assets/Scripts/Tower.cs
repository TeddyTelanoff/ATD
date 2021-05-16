using System;
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

public enum TowerId: int
{
	Soldier,
	Washer,
}

public abstract class Tower: MonoBehaviour
{
	public abstract string Name { get; }
	public abstract TowerId Id { get; }
	public abstract int Price { get; }
	public int SellPrice { get => Mathf.RoundToInt(invested * 0.8f); }

	public Sprite[] spritesPath1 = new Sprite[6];
	public Sprite[] spritesPath2 = new Sprite[6];
	public Sprite[] spritesPath3 = new Sprite[6];
	public Sprite[][] sprites;
	public GameObject dartPrefab;
	public DartProperty dartProps;
	public float effectLifetime;
	public float reload;
	public float kb;
	public int dps;
	public int damage;
	public int pierce;
	public int invested;

	[Header("Don t Touch")]
	public List<Ant> antsInRange;
	public bool placing;

	public Path primPath;
	public Path disPath;
	public Tier path1Tier;
	public Tier path2Tier;
	public Tier path3Tier;

	private void Start()
	{
		invested = Price;
		StartCoroutine(Place());
		sprites = new Sprite[][] { spritesPath1, spritesPath2, spritesPath3, };
	}

	public void Upgrade(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			UpgradePath(path, ref path1Tier, path2Tier, path3Tier, Path.Path2, Path.Path3);
			break;
		case Path.Path2:
			UpgradePath(path, ref path2Tier, path1Tier, path3Tier, Path.Path1, Path.Path3);
			break;
		case Path.Path3:
			UpgradePath(path, ref path3Tier, path1Tier, path2Tier, Path.Path1, Path.Path2);
			break;
		}
	}

	private void UpgradePath(Path path, ref Tier pathTier, Tier tier1, Tier tier2, Path path1, Path path2)
	{
		if (GameManager.Instance.Money < UpgradePrice(path, pathTier))
			return;

		if (disPath == path)
			return;

		if (primPath != Path.None && primPath != path && pathTier >= Tier.Tier2)
			return;

		if (pathTier < Tier.Tier5)
		{
			pathTier++;
			if (primPath == Path.None)
			{
				if (pathTier >= Tier.Tier3)
					primPath = path;
			}

			if (tier1 > Tier.Tier0)
				disPath = path2;
			if (tier2 > Tier.Tier0)
				disPath = path1;

			int price = UpgradePrice(path, pathTier - 1);
			invested += price;
			GameManager.Instance.Money -= price;
			UpgradeInternal(path);
		}
	}	

	protected abstract void UpgradeInternal(Path path);
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
	public Sprite UpgradeSprite(Path path, Tier tier) =>
		sprites[(int)path - 1][(int)tier];

	public Sprite UpgradeSprite(Path path)
	{
		return path switch
		{
			Path.Path1 => UpgradeSprite(path, path1Tier),
			Path.Path2 => UpgradeSprite(path, path2Tier),
			Path.Path3 => UpgradeSprite(path, path3Tier),
			_ => null,
		};
	}

	public Sprite LastUpgradeSprite(Path path)
	{
		return path switch
		{
			Path.Path1 => path1Tier > Tier.Tier0 ? UpgradeSprite(path, path1Tier - 1) : null,
			Path.Path2 => path1Tier > Tier.Tier0 ? UpgradeSprite(path, path2Tier - 1) : null,
			Path.Path3 => path1Tier > Tier.Tier0 ? UpgradeSprite(path, path3Tier - 1) : null,
			_ => null,
		};
	}

	public abstract int UpgradePrice(Path path, Tier tier);
	public int UpgradePrice(Path path)
	{
		return path switch
		{
			Path.Path1 => UpgradePrice(path, path1Tier),
			Path.Path2 => UpgradePrice(path, path2Tier),
			Path.Path3 => UpgradePrice(path, path3Tier),
			_ => 0,
		};
	}

	public Dart Fire(Ant ant)
	{
		Vector2 dir = ant.transform.position - transform.position;
		dir.Normalize();
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

		var obj = Instantiate(dartPrefab);
		obj.transform.position = transform.position;
		obj.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

		var dart = obj.GetComponent<Dart>();
		dart.kb = kb;
		dart.dps = dps;
		dart.dir = dir;
		dart.pierce = pierce;
		dart.damage = damage;
		dart.props = dartProps;
		dart.effectLifetime = effectLifetime;

		return dart;
	}

	protected void TryFireFirst()
	{
		var ant = antsInRange[0];

		try
		{ Fire(ant); }
		catch (NullReferenceException)
		{
			antsInRange.RemoveAll(item => item == null);
			if (antsInRange.Count > 0)
				TryFireFirst();
		}
		catch (MissingReferenceException)
		{
			antsInRange.RemoveAll(item => item == null);
			if (antsInRange.Count > 0)
				TryFireFirst();
		}
	}

	private IEnumerator FireLoop()
	{
		while (true)
		{
			if (antsInRange.Count > 0)
			{
				antsInRange.Sort();
				TryFireFirst();

				yield return new WaitForSeconds(reload);
			}

			yield return new WaitForFixedUpdate();
		}
	}

	private IEnumerator Place()
	{
		placing = true;
		while (placing)
		{
			Vector3 wordPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(wordPos.x, wordPos.y, transform.position.z);

			if (Input.GetMouseButtonUp(0))
			{
				GameManager.Instance.Money -= 200;
				placing = false;
			}
			else if (Input.GetMouseButtonUp(1))
				Destroy(gameObject);

			yield return null;
		}

		StartCoroutine(FireLoop());
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ant"))
		{
			var ant = other.GetComponent<Ant>();
			antsInRange.Add(ant);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ant"))
			antsInRange.Remove(other.GetComponent<Ant>());
	}

	public void Select() =>
		TowerManager.Instance.Select(this);

	public void DeSelect() =>
		TowerManager.Instance.DeSelect();
}