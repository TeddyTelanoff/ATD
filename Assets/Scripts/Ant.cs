using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags, System.Serializable]
public enum AntProperty: int
{
	None,

	Camo = 1 << 0,
}

[System.Serializable]
public enum AntEffect: int
{
	None,

	Flame,
	Wet,
}

[System.Serializable]
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

public partial class Ant: MonoBehaviour
{
	public Transform[] checkpoints;
	public AntType type;
	public float speed;
	public float speedMul;
	public AntEffect effect;
	public AntProperty props;
	public ParticleSystem flameSystem;
	public ParticleSystem wetSystem;
	public AudioSource pop;

	[Header("Don't Touch")]
	public int nextCheckIndex;
	public Transform nextCheckpoint;
	public Vector3 dir;

	private void Start()
	{
		nextCheckpoint = checkpoints[nextCheckIndex];
		UpdateType();

		if (effect == AntEffect.Flame)
			flameSystem.Play();
		StartCoroutine(DPSLoop());
	}

	private void FixedUpdate()
	{
		speedMul = effect == AntEffect.Wet ? 0.5f : 1f;
		dir = nextCheckpoint.position - transform.position;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);

		if (dir.sqrMagnitude <= speedMul * speedMul * speed * speed * GameManager.FixedDeltaTime * GameManager.FixedDeltaTime)
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
			transform.position += speedMul * speed * dir.normalized * GameManager.FixedDeltaTime;
	}

	public void Split()
	{
		var ant = AntSpawner.Instance.SpawnAnt(type);
		ant.transform.position = transform.position + (Vector3)Random.insideUnitCircle;
		ant.nextCheckIndex = nextCheckIndex;
		ant.props = props;
	}

	public void Pop(Dart dart)
	{
		if (props.HasFlag(AntProperty.Camo) && !dart.props.HasFlag(DartProperty.Camo))
			return;

		if (dart.props.HasFlag(DartProperty.Flame))
		{
			effect = AntEffect.Flame;
			wetSystem.Stop();
			flameSystem.Play();
		}

		if (dart.props.HasFlag(DartProperty.Wet))
		{
			effect = AntEffect.Wet;
			flameSystem.Stop();
			wetSystem.Play();
			dart.pierce--;
		}

		Pop(dart.damage);
	}

	public void Pop(int damage)
	{
		if ((int)type <= damage)
			Destroy(gameObject);

		int takeAway = (int)type - damage - 1;
		GameManager.Instance.Money += takeAway;
		type -= takeAway;

		Pop();
	}

	public void Pop()
	{
		GameManager.Instance.Money++;
		pop.Play();

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
			break;
		case AntType.Yellow:
			type = AntType.Blue;
			Split();
			type = AntType.Green;
			Split();
			break;
		case AntType.Brown:
			type = AntType.Green;
			Split();
			Split();
			Split();
			Split();
			type = AntType.Blue;
			Split();
			break;
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

	private IEnumerator DPSLoop()
	{
		while (true)
		{
			if (effect == AntEffect.Flame)
				Pop();

			yield return new WaitForSeconds(1);
		}
	}

	private void OnDrawGizmos()
	{
		if (nextCheckpoint)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, nextCheckpoint.position);
		}
	}
}