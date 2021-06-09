using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum AntProperty: int
{
	None,

	Camo = 1 << 0,
	Armor = 1 << 1,
}

public enum AntEffect: int
{
	None,

	Flame,
	Wet,
}

public enum AntType: int
{
	None,

	Black,
	White,
	Blue,
	Green,
	Yellow,
	Pink,
	Brown,
}

public partial class Ant: MonoBehaviour
{
	public Transform[] checkpoints;
	public GameObject camoModel;
	public GameObject defModel;
	public AntType type;
	public int dps;
	public int hp;
	public float lerp;
	public float speed;
	public float speedMul;
	public float stick;
	public AntEffect effect;
	public AntProperty props;
	public ParticleSystem flameSystem;
	public ParticleSystem wetSystem;
	public AudioSource pop;

	[Header("Don't Touch")]
	public GameObject model;
	public int nextCheckIndex;
	public Transform nextCheckpoint;
	public Vector3 dir;

	private void Start()
	{
		nextCheckpoint = checkpoints[nextCheckIndex];
		dir = nextCheckpoint.position - transform.position;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
		UpdateType();

		if (effect == AntEffect.Flame)
			flameSystem.Play();
		StartCoroutine(DPSLoop());
	}

	private void FixedUpdate()
	{
		speedMul = effect == AntEffect.Wet ? 0.5f : 1f;
		dir = nextCheckpoint.position - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90), lerp);

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
			return;
		}

		if (stick <= 0)
		{
			effect &= ~AntEffect.Flame;
			effect &= ~AntEffect.Wet;
		}
		else
			stick -= Time.deltaTime;
		transform.position += speedMul * speed * dir.normalized * Time.deltaTime;
	}

	public void Split()
	{
		var ant = AntSpawner.Instance.SpawnAnt(type);
		ant.transform.position = transform.position + (Vector3)Random.insideUnitCircle;
		ant.nextCheckIndex = nextCheckIndex;
		ant.props = props;
	}
	public void Pop(Explosion dart)
	{
		if (dart.props.HasFlag(DartProperty.Rinse))
		{
			props &= ~AntProperty.Camo;
			props &= ~AntProperty.Armor;

			UpdateType();
		}

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
			dps = dart.dps;

			stick = dart.stick;
		}

		Pop(dart.damage);
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
			dps = dart.dps;
			dart.pierce--;

			stick = dart.stick;
		}

		transform.position += dart.kb * (Vector3)(Vector2)dart.dir;
		Pop(dart.damage);
	}

	public void Pop(int damage)
	{
		for (int i = 0; i < damage; i++)
			Pop();
	}

	public void Pop()
	{
		pop.Play();
		hp--;

		if (hp > 0)
			return;

		GameManager.Instance.money++;

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
			type = AntType.Blue;
			break;
		case AntType.Yellow:
			type = AntType.Green;
			break;
		case AntType.Pink:
			type = AntType.Yellow;
			break;
		case AntType.Brown:
			type = AntType.Pink;
			Split();
			break;
		}

		UpdateType();
	}

	public void UpdateType()
	{
		hp = 1;
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
		case AntType.Pink:
			speed = 10;
			matCol = Color.magenta;
			break;
		case AntType.Brown:
			hp = 10;
			speed = 5;
			matCol = new Color(0.8f, 0.4f, 0.1f);
			break;
		}

		if (props.HasFlag(AntProperty.Camo))
		{
			defModel.SetActive(false);
			camoModel.SetActive(true);
			model = camoModel;
		}
		else
		{
			camoModel.SetActive(false);
			defModel.SetActive(true);
			model = defModel;
		}

		var mat = model.GetComponentInChildren<MeshRenderer>().material;
		mat.color = matCol;
	}

	private IEnumerator DPSLoop()
	{
		while (true)
		{
			if (effect == AntEffect.Flame)
				Pop();
			if (effect == AntEffect.Wet)
				Pop(dps);

			yield return new WaitForSeconds(1);
		}
	}

	private void OnDrawGizmos()
	{
		if (!nextCheckpoint)
			return;

		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position, nextCheckpoint.position);
	}
}