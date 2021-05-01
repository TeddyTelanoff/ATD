using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AntType: int
{
	None,

	Black,
	White,
	Blue,
	Green,
	Yellow,
	Brown,
}

public class Ant: MonoBehaviour
{
	public Transform[] checkpoints;
	public AntType type;
	public float speed;

	[Header("Don't Touch")]
	public int nextCheckIndex;
	public Transform nextCheckpoint;

	private void Start()
	{
		nextCheckpoint = checkpoints[nextCheckIndex];
		UpdateType();
	}

	private void FixedUpdate()
	{
		Vector3 dir = nextCheckpoint.position - transform.position;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);

		if (dir.sqrMagnitude < speed * speed * Time.deltaTime * Time.deltaTime)
		{
			transform.position = nextCheckpoint.position;
			nextCheckIndex++;
			if (nextCheckIndex >= checkpoints.Length)
			{
				GameManager.Instance.Health -= (int)type;
				Destroy(gameObject);
				return;
			}
			nextCheckpoint = checkpoints[nextCheckIndex];
		}
		else
			transform.position += speed * dir.normalized * Time.deltaTime;
	}

	public void Pop(Dart dart)
	{
		for (int i = 0; i < dart.damage; i++)
		{
			GameManager.Instance.Money++;

			switch (type)
			{
			case AntType.Black:
				Destroy(gameObject);
				return;
			case AntType.White:
				type = AntType.Black;
				break;
			case AntType.Blue:
				type = AntType.White;
				break;
			case AntType.Green:
				type = AntType.White;
				Instantiate(this).transform.position = transform.position;
				break;
			case AntType.Yellow:
				type = AntType.Blue;
				Instantiate(this).transform.position = transform.position;
				type = AntType.Green;
				Instantiate(this).transform.position = transform.position;
				break;
			case AntType.Brown:
				type = AntType.Green;
				Instantiate(this).transform.position = transform.position;
				Instantiate(this).transform.position = transform.position;
				Instantiate(this).transform.position = transform.position;
				Instantiate(this).transform.position = transform.position;
				type = AntType.Blue;
				Instantiate(this).transform.position = transform.position;
				break;
			}
		}

		UpdateType();
	}

	public void UpdateType()
	{
		Color matCol = Color.black;
		switch (type)
		{
		case AntType.Black:
			speed = 3;
			matCol = Color.gray;
			break;
		case AntType.White:
			speed = 3;
			matCol = Color.white;
			break;
		case AntType.Blue:
			speed = 4;
			matCol = Color.blue;
			break;
		case AntType.Green:
			speed = 4;
			matCol = Color.green;
			break;
		case AntType.Yellow:
			speed = 8;
			matCol = Color.yellow;
			break;
		case AntType.Brown:
			speed = 5;
			matCol = new Color(210, 105, 30);
			break;
		}

		GetComponent<Renderer>().material.color = matCol;
	}

	private void OnDrawGizmos()
	{
		if (!(nextCheckpoint is null))
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, nextCheckpoint.position);
		}
	}
}