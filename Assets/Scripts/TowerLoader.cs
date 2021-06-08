using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public partial class TowerManager
{
	public string resourceLocation;

	public void LoadAll()
	{
		var towerStats = Directory.GetDirectories($"{resourceLocation}/Towers");
		if (towerStats.Length <= 0)
			return;

		towerData = new TowerData[towerStats.Length];
		for (int i = 0; i < towerStats.Length; i++)
		{
			towerStats[i] = towerStats[i].Substring(resourceLocation.Length + 1);
			towerData[i] = Load(towerStats[i]);
			towerData[i].file = towerStats[i];
		}

		Choose(0);
	}

	public Sprite LoadSprite(string path) =>
		Resources.Load<Sprite>(path);

	public TowerData Load(string path)
	{
		var data = JsonConvert.DeserializeObject<TowerData>(Resources.Load<TextAsset>($"{path}/Tower").text);
		LoadTower(path, ref data);

		return data;
	}

	public void LoadTower(string path, ref TowerData data)
	{
		data.mesh = Resources.Load<GameObject>($"{path}/Model");

		data.icon = LoadSprite("{path}/Icon");
		if (data.path1 != null)
			for (int i = 0; i < data.path1.Length; i++)
				data.path1[i].sprite = LoadSprite($"{path}/1-{i + 1}");
		if (data.path2 != null)
			for (int i = 0; i < data.path2.Length; i++)
				data.path2[i].sprite = LoadSprite($"{path}/2-{i + 1}");
		if (data.path3 != null)
			for (int i = 0; i < data.path3.Length; i++)
				data.path3[i].sprite = LoadSprite($"{path}/3-{i + 1}");

		var tmp = new Upgrade[6];
		if (data.path1 != null)
			for (int i = 0; i < 6; i++)
				if (i < data.path1.Length)
					tmp[i] = data.path1[i];
				else
					tmp[i] = data.path1[data.path1.Length - 1];
		data.path1 = tmp;
		tmp = new Upgrade[6];
		if (data.path2 != null)
			for (int i = 0; i < 6; i++)
				if (i < data.path2.Length)
					tmp[i] = data.path2[i];
				else
					tmp[i] = data.path2[data.path2.Length - 1];
		data.path2 = tmp;
		tmp = new Upgrade[6];
		if (data.path3 != null)
			for (int i = 0; i < 6; i++)
				if (i < data.path3.Length)
					tmp[i] = data.path3[i];
				else
					tmp[i] = data.path3[data.path3.Length - 1];
		data.path3 = tmp;
	}
}

public partial class Tower
{
	public Transform model;

	public void Load()
	{
		if (data.mesh)
		{
			Instantiate(data.mesh, model);
			GetComponent<SpriteRenderer>().enabled = false;
		}

		invested = data.price;
		transform.localScale = Vector3.one * data.range;

		dartProps = data.props;
		effectLifetime = data.effectLifetime;
		reload = data.reload;
		kb = data.kb;
		dps = data.dps;
		damage = data.damage;
		pierce = data.pierce;
		range = data.range;
	}
}
