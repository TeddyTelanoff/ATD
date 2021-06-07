using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
	public ParticleSystem system;
	public DartProperty props;
	public float effectLifetime;
	public float explosion;
	public int damage;
	public int dps;

	private void Start()
	{
		StartCoroutine(SayGoodbye());
		system.transform.localScale = Vector3.one * explosion * 0.2f;
		transform.localScale = Vector3.one * explosion;
	}

	private IEnumerator SayGoodbye()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitWhile(() => system.isPlaying);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (system.isPlaying && other.gameObject.layer == LayerMask.NameToLayer("Ant"))
			other.GetComponent<Ant>().Pop(this);
	}
}
