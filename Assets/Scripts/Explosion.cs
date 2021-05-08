using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
	public ParticleSystem system;
	public DartProperty props;
	public int damage;

	[Header("I have CUTIES, plz don't tuch")]
	public bool attacking;

	private void Start() =>
		StartCoroutine(SayGoodbye());

	private IEnumerator SayGoodbye()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitWhile(() => system.isPlaying);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (attacking && other.gameObject.layer == LayerMask.NameToLayer("Ant"))
			other.GetComponent<Ant>().Pop(this);
	}
}
