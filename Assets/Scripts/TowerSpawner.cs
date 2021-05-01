using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
	public GameObject soldierPrefab;

	[Header("No Peeking")]
	public bool spawning;

	public void Spawn() =>
		spawning = true;

	public void FixedUpdate()
	{
		if (spawning)
		{
			Instantiate(soldierPrefab);
			spawning = false;
		}
	}
}
