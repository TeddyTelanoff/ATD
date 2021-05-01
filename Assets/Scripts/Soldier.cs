using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier: MonoBehaviour, Tower
{
	public GameObject dartPrefab;
	public float reloadSpeed;

	[Header("Don t Touch")]
	public List<Ant> antsInRange;

	private void Start() =>
		StartCoroutine(FireLoop());

	public void Fire(Ant ant)
	{
		Vector2 dir = ant.transform.position - transform.position;
		dir.Normalize();
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

		var dart = Instantiate(dartPrefab);
		dart.transform.position = transform.position;
		dart.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

		dart.GetComponent<Dart>().dir = dir;
	}

	private IEnumerator FireLoop()
	{
		while (true)
		{
			if (antsInRange.Count > 0)
			{
				var ant = antsInRange[0];
				Fire(ant);

				yield return new WaitForSeconds(reloadSpeed);
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
}
