using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle: MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		print(other.tag);
		if (other.CompareTag("Placement"))
			other.GetComponent<TowerPlacement>().tower.obsTouching++;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Placement"))
			other.GetComponent<TowerPlacement>().tower.obsTouching--;
	}
}
