using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ant"))
			other.GetComponent<Ant>().Pop();
	}
}
