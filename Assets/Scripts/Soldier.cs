using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier: Tower
{
	public override string Name { get => "Soldier"; }

	public GameObject dartPrefab;
	public float reload;
	public int damage;

	[Header("Don t Touch")]
	public List<Ant> antsInRange;
	public bool placing;

	private void Start() =>
		StartCoroutine(FireLoop());

	protected override void UpgradeInternal(Path path)
	{
		switch (path)
		{
		case Path.Path1:
			switch (path1Tier)
			{
			case Tier.Tier1:
				pierce++;
				break;
			case Tier.Tier2:
				damage++;
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
				transform.Find("View").localScale = new Vector3(1, 1, 1);
				break;
			case Tier.Tier3:
				pierce += 3;
				break;
			case Tier.Tier4:
				transform.Find("View").localScale = new Vector3(99, 99, 1);
				break;
			case Tier.Tier5:
				pierce += 95;
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
				Tier.Tier4 => 13000,
				_ => 0,
			},

			_ => 0,
		};
	}

	public override void Fire(Ant ant)
	{
		try
		{
			Vector2 dir = ant.transform.position - transform.position;
			dir.Normalize();
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

			var dart = Instantiate(dartPrefab);
			dart.transform.position = transform.position;
			dart.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

			dart.GetComponent<Dart>().dir = dir;
			dart.GetComponent<Dart>().pierce = pierce;
			dart.GetComponent<Dart>().damage = damage;
		}
		catch (NullReferenceException)
		{ antsInRange.RemoveAll(item => item == null); }
		catch (MissingReferenceException)
		{ antsInRange.RemoveAll(item => item == null); }
	}

	private IEnumerator FireLoop()
	{
		while (placing)
		{
			Vector3 wordPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(wordPos.x, wordPos.y, transform.position.z);

			if (Input.GetMouseButtonUp(0))
			{
				GameManager.Instance.Money -= 350;
				placing = false;
			}
			else if (Input.GetMouseButtonUp(1))
				Destroy(gameObject);

			yield return null;
		}

		while (true)
		{
			if (antsInRange.Count > 0)
			{
				var ant = antsInRange[0];
				Fire(ant);

				yield return new WaitForSeconds(reload);
			}

			yield return new WaitForFixedUpdate();
		}
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

	private void OnMouseUpAsButton()
	{
		TowerManager.Instance.Select(this);
	}
}
