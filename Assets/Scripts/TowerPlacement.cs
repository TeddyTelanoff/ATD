using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement: MonoBehaviour
{
	public Tower tower;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Obsticle"))
			tower.obsTouching++;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Obsticle"))
			tower.obsTouching--;
	}
}
