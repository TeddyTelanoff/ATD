using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner: MonoBehaviour
{
	public Transform[] checkpoints;
	public GameObject antPrefab;
	public float delay;

	private void Start() =>
		StartCoroutine(Spawn());

	private IEnumerator Spawn()
	{
		while (true)
		{
			var ant = Instantiate(antPrefab);
			ant.transform.position = transform.position;
			ant.GetComponent<Ant>().checkpoints = checkpoints;

			yield return new WaitForSeconds(delay);
		}
	}
}
