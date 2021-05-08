using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum DartProperty
{
	None,

	Camo = 1 << 0,
	Flame = 1 << 1,
	Wet = 1 << 2,
}

public enum DartType
{
	Sharp,
	Explosive,
	HyperSonic,
}

public class Dart: MonoBehaviour
{
	public DartProperty props;
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

		rb.AddForce(speed * dir * GameManager.FixedDeltaTime, ForceMode2D.Impulse);
		timeout -= GameManager.FixedDeltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (pierce > 0 && other.gameObject.layer == LayerMask.NameToLayer("Ant"))
		{
			other.GetComponent<Ant>().Pop(this);

			if (pierce <= 0)
				Destroy(gameObject);
		}
	}
}
