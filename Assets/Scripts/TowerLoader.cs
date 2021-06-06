using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public partial class Tower
{
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

	public void Load()
	{
		data = JsonConvert.DeserializeObject<TowerData>(File.ReadAllText($"Assets/Towers/{dataFile}.json"));
		data.sprite = LoadSprite($"Assets/Sprites/{data.image}");
		for (int i = 0; i < data.path1.Length; i++)
			data.path1[i].sprite = LoadSprite($"Assets/Sprites/{data.path1[i].image}");
		for (int i = 0; i < data.path2.Length; i++)
			data.path2[i].sprite = LoadSprite($"Assets/Sprites/{data.path2[i].image}");
		for (int i = 0; i < data.path3.Length; i++)
			data.path3[i].sprite = LoadSprite($"Assets/Sprites/{data.path3[i].image}");

		invested = data.price;
		transform.localScale = Vector3.one * data.range;

		dartProps = data.props;
		effectLifetime = data.effectLifetime;
		reload = data.reload;
		kb = data.kb;
		dps = data.dps;
		damage = data.damage;
		pierce = data.pierce;
	}
}
