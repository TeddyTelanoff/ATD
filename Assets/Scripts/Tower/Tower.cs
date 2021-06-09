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

public partial class Tower: MonoBehaviour
{
	public GameObject placement;
	public GameObject dartPrefab;
	public Transform view;
	public Transform selectable;

	[Header("Don t Touch")]
	public TowerData data;
	public DartProperty dartProps;
	public float stick;
	public float explosion;
	public float dartSpeed;
	public float reload;
	public float range { get => _range;
		set
		{
			_range = value;
			view.localScale = Vector3.one * value;
		}
	}
	public float kb;
	public int dps;
	public int blast;
	public int damage;
	public int pierce;
	public int invested;

	public List<Ant> antsInRange;
	public bool placing;
	public int obsTouching;

	private float _range;

	private void Start()
	{
		StartCoroutine(Place());
		Load();
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
				antsInRange.Sort(AntTarget.First);
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

			if (Input.GetMouseButtonUp(1))
				Destroy(gameObject);

			if (obsTouching == 0)
			{
				view.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.1333f);

				if (Input.GetMouseButtonUp(0))
					placing = false;
			}
			else
				view.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.25f);

			yield return null;
		}

		StartCoroutine(FireLoop());
		selectable.GetComponent<Collider2D>().enabled = true;
		view.GetComponent<Collider2D>().enabled = true;
		TowerManager.Instance.placingTower = null;
		GameManager.Instance.money -= data.price;
		GetComponent<Rigidbody2D>().WakeUp();
		TowerManager.Instance.Select(this);

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