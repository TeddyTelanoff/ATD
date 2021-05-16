using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AntSpawner: MonoBehaviour
{
	public static AntSpawner Instance { get; private set; }

	public AudioSource antPop;
	public Transform parent;
	public Transform[] checkpoints;
	public GameObject antPrefab;
	public TMP_InputField roundTxt;
	public int round;

	private void Start()
	{
		Instance = this;
		roundTxt.text = round.ToString();
	}

	public void EarthQuake() =>
		StartCoroutine(CoEarthQuake());

	public IEnumerator CoEarthQuake()
	{
		float start = Time.time;
		while (Time.time - start < 1f)
		{
			for (int i = 0; i < parent.childCount; i++)
				parent.GetChild(i).position += (Vector3)UnityEngine.Random.insideUnitCircle;

			yield return new WaitForFixedUpdate();
		}
	}

	public void SetRoundFromInput()
	{
		if (int.TryParse(roundTxt.text, out round))
			return;

		round = 1;
		roundTxt.text = "1";
	}

	public void PlayRound(int roundNum)
	{
		round = roundNum;
		PlayRound();
	}

	public void PlayRound() =>
		StartCoroutine(PlayRoundInternal());

	public Boolean RoundOver() =>
		parent.childCount <= 0;

	public Ant SpawnAnt(AntType type, AntProperty props = AntProperty.None)
	{
		var obj = Instantiate(antPrefab, parent);
		obj.transform.position = checkpoints[0].position;

		var ant = obj.GetComponent<Ant>();
		ant.checkpoints = checkpoints;
		ant.props = props;
		ant.pop = antPop;
		ant.type = type;
		return ant;
	}

	public void SpawnAnts(AntType type, int count)
	{
		for (int i = 0; i < count; i++)
			SpawnAnt(type);
	}

	public void SpawnAnts(AntType type, int count, float interval) =>
		StartCoroutine(CoSpawnAnts(type, count, interval));

	private IEnumerator CoSpawnAnts(AntType type, int count, float interval)
	{
		for (int i = 0; i < count; i++)
		{
			SpawnAnt(type);
			yield return new WaitForSeconds(interval);
		}
	}

	private IEnumerator PlayRoundInternal()
	{
		int moneyEarned = 100;

		switch (round)
		{
		case 1:
			SpawnAnts(AntType.Black, 14, 1.2f);
			break;
		case 2:
			SpawnAnts(AntType.Black, 12, 1f);
			break;
		case 3:
			SpawnAnts(AntType.White, 6, 1.6f);
			break;
		case 4:
			SpawnAnts(AntType.Black, 20, 1f);
			break;
		case 5:
			SpawnAnts(AntType.Black, 10, 1f);
			SpawnAnts(AntType.White, 5, 1.6f);
			break;
		case 6:
			SpawnAnts(AntType.White, 5, 1f);
			break;
		case 7:
			SpawnAnts(AntType.Blue, 10, 1.6f);
			break;
		case 8:
			SpawnAnts(AntType.White, 10, 1.2f);
			SpawnAnts(AntType.Green, 5, 1.6f);
			break;
		case 9:
			SpawnAnts(AntType.Green, 10, 1.6f);
			break;
		case 10:
			SpawnAnt(AntType.Yellow);
			moneyEarned = 150;
			break;
		case 11:
			for (int i = 0; i < 11; i++)
			{
				SpawnAnt(AntType.Blue);
				SpawnAnt(AntType.White);
				SpawnAnt(AntType.Black);
				yield return new WaitForSeconds(1f);
			}
			moneyEarned = 150;
			break;
		case 12:
			SpawnAnts(AntType.Blue, 20, 1.2f);
			break;
		case 13:
			SpawnAnts(AntType.Blue, 50, 0.75f);
			SpawnAnts(AntType.White, 25, 0.75f);
			moneyEarned = 300;
			break;
		case 14:
			SpawnAnts(AntType.Blue, 100, 1f);
			moneyEarned = 200;
			break;
		case 15:
			SpawnAnts(AntType.Yellow, 10, 1f);
			moneyEarned = 150;
			break;
		case 16:
			SpawnAnts(AntType.Blue, 10);
			break;
		case 17:
			SpawnAnts(AntType.Green, 40, 1.6f);
			break;
		case 18:
			SpawnAnt(AntType.Black, AntProperty.Camo);
			break;
		case 19:
			SpawnAnt(AntType.Yellow, AntProperty.Camo);
			break;
		case 20:
			SpawnAnts(AntType.Brown, 1);
			break;
		}

		while (!RoundOver())
			yield return new WaitForFixedUpdate();
		GameManager.Instance.Money += moneyEarned;
		round++;

		roundTxt.text = round.ToString();
	}
}
