using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public partial class AntSpawner
{
	public void LoadRounds() =>
		rounds = JsonConvert.DeserializeObject<Round[]>(Resources.Load<TextAsset>("Rounds").text);
}