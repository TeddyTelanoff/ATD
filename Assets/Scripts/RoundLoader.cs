using System;
using UnityEngine;

public partial class AntSpawner
{
	public void LoadRounds()
	{
		string[] lines = Resources.Load<TextAsset>("Rounds").text.Split('\n');
		rounds = new Round[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			int colon = lines[i].IndexOf(':');
			rounds[i].reward = int.Parse(lines[i].Substring(0, colon));
			string[] waves = lines[i].Substring(colon + 1).Split(';');
			rounds[i].waves = new Wave[waves.Length];
			for (int j = 0; j < waves.Length; j++)
			{
				string[] words = waves[j].Split(' ');
				print(words[0]);
				rounds[i].waves[j].count = int.Parse(words[0]);
				rounds[i].waves[j].type = (AntType)Enum.Parse(typeof(AntType), words[1], true);
				rounds[i].waves[j].interval = float.Parse(words[2]);
			}
		}
	}
}