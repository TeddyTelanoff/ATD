using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart: MonoBehaviour
{
	public int pierce;
	public float speed;
	public float timeout;

	[Header("NO TOUCH")]
	public Vector3 dir;
	public Rigidbody2D rb;

	private void Start() =>
		rb = GetComponent<Rigidbody2D>();

	private void FixedUpdate()
	{
		if (timeout <= 0)
			Destroy(gameObject);

		rb.AddForce(speed * dir * Time.deltaTime, ForceMode2D.Impulse);
		timeout -= Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (pierce > 0 && other.gameObject.layer == LayerMask.NameToLayer("Ant"))
		{
			other.GetComponent<Ant>().Pop();
			pierce--;

			if (pierce <= 0)
				Destroy(gameObject);
		}
	}
}
