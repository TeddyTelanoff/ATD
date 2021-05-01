using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AntSpawner: MonoBehaviour
{
	public Transform[] checkpoints;
	public GameObject antPrefab;
	public int round;

	public void SetRoundFromInput(TMP_InputField input)
	{
		if (!int.TryParse(input.text, out round))
		{
			round = 0;
			input.text = "0";
		}
	}

	public void PlayRound(int roundNum)
	{
		round = roundNum;
		PlayRound();
	}

	public void PlayRound() =>
		StartCoroutine(PlayRoundInternal());

	public void SpawnAnt()
	{
		var ant = Instantiate(antPrefab);
		ant.transform.position = checkpoints[0].position;
		ant.GetComponent<Ant>().checkpoints = checkpoints;
	}

	private IEnumerator PlayRoundInternal()
	{
		switch (round)
		{
		case 1:
			for (int i = 0; i < 14; i++)
			{
				SpawnAnt();
				yield return new WaitForSeconds(1.2f);
			}
			break;
		case 2:
			for (int i = 0; i < 16; i++)
			{
				SpawnAnt();
				yield return new WaitForSeconds(1f);
			}
			break;
		case 3:
			for (int i = 0; i < 6; i++)
			{
				SpawnAnt();
				yield return new WaitForSeconds(1.6f);
			}
			break;
		}

		yield return null;
	}
}
