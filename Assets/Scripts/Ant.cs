using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant: MonoBehaviour
{
	public Transform[] checkpoints;
	public AntColor color;
	public float speed;

	[Header("Don't Touch")]
	public int nextCheckIndex;
	public Transform nextCheckpoint;
	public float speedSqrd;

	private void Start()
	{
		nextCheckpoint = checkpoints[0];
		speedSqrd = speed * speed;

		UpdateColor();
	}

	private void FixedUpdate()
	{
		Vector3 dir = nextCheckpoint.position - transform.position;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);

		if (dir.sqrMagnitude < speedSqrd)
		{
			transform.position = nextCheckpoint.position;
			nextCheckIndex++;
			if (nextCheckIndex >= checkpoints.Length)
			{
				print("Leaked!");
				Destroy(gameObject);
				return;
			}
			nextCheckpoint = checkpoints[nextCheckIndex];
		}
		else
			transform.position += speed * dir.normalized * Time.deltaTime;
	}

	public void Pop()
	{
		switch (color)
		{
		case AntColor.Black:
			Destroy(gameObject);
			break;
		case AntColor.White:
			color = AntColor.Black;
			break;
		case AntColor.Blue:
			color = AntColor.White;
			break;
		}

		UpdateColor();
	}

	public void UpdateColor()
	{
		Color matCol = Color.black;
		switch (color)
		{
		case AntColor.Black:
			matCol = Color.gray;
			break;
		case AntColor.White:
			matCol = Color.white;
			break;
		case AntColor.Blue:
			matCol = Color.blue;
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

public enum AntColor
{
	Black,
	White,
	Blue,
}