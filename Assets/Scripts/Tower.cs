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

public class Tower: MonoBehaviour
{
	public int SellPrice { get => Mathf.RoundToInt(invested * 0.8f); }

	public TowerData data;

	public GameObject placement;
	public GameObject dartPrefab;
	public Transform view;

	[Header("Don t Touch")]
	public DartProperty dartProps;
	public float effectLifetime;
	public float reload;
	public float kb;
	public int dps;
	public int damage;
	public int pierce;
	public int invested;

	public List<Ant> antsInRange;
	public bool placing;
	public int obsTouching;

	public Path primPath;
	public Path disPath;
	public Tier path1Tier;
	public Tier path2Tier;
	public Tier path3Tier;

	private void Start()
	{
		invested = data.price;
		transform.localScale = Vector3.one * data.range;
		StartCoroutine(Place());

		dartProps = data.props;
		effectLifetime = data.effectLifetime;
		reload = data.reload;
		kb = data.kb;
		dps = data.dps;
		damage = data.damage;
		pierce = data.pierce;
		view.localScale = Vector3.one * data.range;
	}

	public void Upgrade(Upgrade upgrade)
	{
		dartProps |= upgrade.props;
		effectLifetime += upgrade.effectLifetime;
		reload += upgrade.reload;
		kb += upgrade.kb;
		dps += upgrade.dps;
		damage += upgrade.damage;
		pierce += upgrade.pierce;
		view.localScale += Vector3.one * upgrade.range;
	}

	public void Upgrade(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			if (UpgradePath(path, ref path1Tier, path2Tier, path3Tier, Path.Path2, Path.Path3))
				Upgrade(data.upgradesPath1[(int)path1Tier - 1]);
			break;
		case Path.Path2:
			if (UpgradePath(path, ref path2Tier, path1Tier, path3Tier, Path.Path1, Path.Path3))
				Upgrade(data.upgradesPath2[(int)path2Tier - 1]);
			break;
		case Path.Path3:
			if (UpgradePath(path, ref path3Tier, path1Tier, path2Tier, Path.Path1, Path.Path2))
				Upgrade(data.upgradesPath3[(int)path3Tier - 1]);
			break;
		}
	}

	private bool UpgradePath(Path path, ref Tier pathTier, Tier tier1, Tier tier2, Path path1, Path path2)
	{
		if (GameManager.Instance.Money < UpgradePrice(path, pathTier))
			return false;

		if (disPath == path)
			return false;

		if (primPath != Path.None && primPath != path && pathTier >= Tier.Tier2)
			return false;

		if (pathTier >= Tier.Tier5)
			return false;
		
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
		return true;
	}	

	public string UpgradeName(Path path, Tier tier) =>
		path switch
		{
			Path.Path1 => data.upgradesPath1[(int)tier].name,
			Path.Path2 => data.upgradesPath2[(int)tier].name,
			Path.Path3 => data.upgradesPath3[(int)tier].name,
			_ => null,
		};

	public string UpgradeName(Path path) =>
		path switch
		{
			Path.Path1 => data.upgradesPath1[(int)path1Tier].name,
			Path.Path2 => data.upgradesPath2[(int)path2Tier].name,
			Path.Path3 => data.upgradesPath3[(int)path3Tier].name,
			_ => null,
		};

	public Sprite UpgradeSprite(Path path, Tier tier) =>
		path switch
		{
			Path.Path1 => data.upgradesPath1[(int)tier].sprite,
			Path.Path2 => data.upgradesPath2[(int)tier].sprite,
			Path.Path3 => data.upgradesPath3[(int)tier].sprite,
			_ => null,
		};

	public Sprite UpgradeSprite(Path path) =>
		path switch
		{
			Path.Path1 => data.upgradesPath1[(int)path1Tier].sprite,
			Path.Path2 => data.upgradesPath2[(int)path2Tier].sprite,
			Path.Path3 => data.upgradesPath3[(int)path3Tier].sprite,
			_ => null,
		};

	public Sprite LastUpgradeSprite(Path path) =>
		path switch
		{
			Path.Path1 => path1Tier > Tier.Tier0 ? UpgradeSprite(path, path1Tier - 1) : null,
			Path.Path2 => path1Tier > Tier.Tier0 ? UpgradeSprite(path, path2Tier - 1) : null,
			Path.Path3 => path1Tier > Tier.Tier0 ? UpgradeSprite(path, path3Tier - 1) : null,
			_ => null,
		};

	public int UpgradePrice(Path path, Tier tier) =>
		path switch
		{
			Path.Path1 => data.upgradesPath1[(int)tier].price,
			Path.Path2 => data.upgradesPath2[(int)tier].price,
			Path.Path3 => data.upgradesPath3[(int)tier].price,
			_ => 0,
		};

	public int UpgradePrice(Path path) =>
		path switch
		{
			Path.Path1 => data.upgradesPath1[(int)path1Tier].price,
			Path.Path2 => data.upgradesPath2[(int)path2Tier].price,
			Path.Path3 => data.upgradesPath3[(int)path3Tier].price,
			_ => 0,
		};

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
		dart.timeout = transform.localScale.x;
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

			if (obsTouching <= 0)
			{
				view.GetComponent<Renderer>().material.color = Color.red;

				if (Input.GetMouseButtonUp(0))
				{
					TowerManager.Instance.placingTower = null;
					GetComponent<Rigidbody2D>().WakeUp();
					GameManager.Instance.Money -= 200;
					placing = false;
				}
				else if (Input.GetMouseButtonUp(1))
					Destroy(gameObject);
			}
			else
				view.GetComponent<Renderer>().material.color = Color.grey;

			yield return null;
		}

		StartCoroutine(FireLoop());
		Destroy(placement);
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