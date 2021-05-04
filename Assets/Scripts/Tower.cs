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

public abstract class Tower: MonoBehaviour
{
	public abstract string Name { get; }

	[Header("Don t Touch")]
	public List<Ant> antsInRange;
	public bool placing;

	public int pierce;
	public Path primPath;
	public Path disPath;
	public Tier path1Tier;
	public Tier path2Tier;
	public Tier path3Tier;

	private void Start() =>
		StartCoroutine(Place());

	public void Upgrade(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			if (GameManager.Instance.Money < UpgradePrice(path, path1Tier))
				break;

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

				GameManager.Instance.Money -= UpgradePrice(path, path1Tier - 1);
				UpgradeInternal(path);
			}
			break;
		case Path.Path2:
			if (GameManager.Instance.Money < UpgradePrice(path, path2Tier))
				break;

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

				GameManager.Instance.Money -= UpgradePrice(path, path2Tier - 1);
				UpgradeInternal(path);
			}
			break;
		case Path.Path3:
			if (GameManager.Instance.Money < UpgradePrice(path, path3Tier))
				break;

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

				GameManager.Instance.Money -= UpgradePrice(path, path3Tier - 1);
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

	protected abstract IEnumerator FireLoop();

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

	private IEnumerator Place()
	{
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
			antsInRange.Add(other.GetComponent<Ant>());
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ant"))
			antsInRange.Remove(other.GetComponent<Ant>());
	}

	public void Select() =>
		TowerManager.Instance.Select(this);
}