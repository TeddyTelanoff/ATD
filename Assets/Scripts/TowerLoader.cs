using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public partial class TowerManager
{
	public void LoadAll()
	{
		var towerStats = Directory.GetDirectories(towerStatsLoc);
		loadedStats = new TowerData[towerStats.Length];
		for (int i = 0; i < towerStats.Length; i++)
		{
			loadedStats[i] = Load(towerStats[i]);
			loadedStats[i].file = towerStats[i];
		}
	}

	public Sprite LoadSprite(string path)
	{
		if (string.IsNullOrEmpty(path))
			return null;
		if (!File.Exists(path))
			return null;

		byte[] bytes = File.ReadAllBytes(path);
		Texture2D texture = new Texture2D(1, 1);
		texture.LoadImage(bytes);
		Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		return sprite;
	}

	public TowerData Load(string path)
	{
		var data = JsonConvert.DeserializeObject<TowerData>(File.ReadAllText($"{path}/Tower.json"));
		LoadTower(path, ref data);

		return data;
	}

	public void LoadTower(string path, ref TowerData data)
	{
		bool found = false;
		var scene = SceneManager.GetSceneByPath(path);
		// TODO: Make it not supa slow
		foreach (var obj in scene.GetRootGameObjects())
			if (obj.name == "Model" &&
				(found = true)) // Neat Trick, huh?
				{
					data.mesh = obj;
					print(obj.scene);
				}

		if (!found)
		{
			data.mesh = Resources.Load<GameObject>($"{path}/Model");
			Debug.Log(data.mesh);
		}

		data._icon = LoadSprite("{path}/Icon.png");
		if (data.path1 != null)
			for (int i = 0; i < data.path1.Length; i++)
				data.path1[i].sprite = LoadSprite($"{path}/1-{i + 1}.png");
		if (data.path2 != null)
			for (int i = 0; i < data.path2.Length; i++)
				data.path2[i].sprite = LoadSprite($"{path}/2-{i + 1}.png");
		if (data.path3 != null)
			for (int i = 0; i < data.path3.Length; i++)
				data.path3[i].sprite = LoadSprite($"{path}/3-{i + 1}.png");

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
