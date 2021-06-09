using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
	public ParticleSystem normal;
	public ParticleSystem wet;
	public DartProperty props;
	public float stick;
	public float explosion;
	public int blast;
	public int damage;
	public int dps;

	[Header("Donut Tuch")]
	public ParticleSystem system;

	private void Start()
	{
		StartCoroutine(SayGoodbye());
		system = props.HasFlag(DartProperty.Wet) ? wet : normal;
		system.gameObject.SetActive(true);

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
		if (blast <= 0)
			return;

		if (system.isPlaying && other.gameObject.layer == LayerMask.NameToLayer("Ant"))
			other.GetComponent<Ant>().Pop(this);
	}
}
