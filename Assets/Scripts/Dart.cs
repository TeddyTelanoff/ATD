using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DartType
{
	Sharp,
	Explosive,
	HyperSonic,
}

public class Dart: MonoBehaviour
{
	public DartType type;
	public int pierce;
	public int damage;
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
			other.GetComponent<Ant>().Pop(this);
			pierce--;

			if (pierce <= 0)
				Destroy(gameObject);
		}
	}
}
