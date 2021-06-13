using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	
	public static GameManager Instance { get; private set; }
	public static float FixedDeltaTime { get => 0.02f; }

	public bool godMode;
	public AudioSource popSound;
	public AudioSource explosionSound;
	public TMP_Text moneyText;
	public int money
	{
		get => godMode ? _money = 999999 : _money;
		set
		{
			_money = value;
			moneyText.text = $"${_money}";
		}
	}
	public TMP_Text healthText;
	public int Health
	{
		get => _health;
		set
		{
			_health = value;
			healthText.text = $"{_health}hp";
		}
	}

	[SerializeField]
	private int _money;
	[SerializeField]
	private int _health;

	private void Start()
	{
		Instance = this;
		moneyText.text = $"${_money}";
		healthText.text = $"{_health}hp";
	}

	public void UpdateSpeed(float speed) =>
		Time.timeScale = speed;

	public void UpdateSpeed(Slider slider) =>
		Time.timeScale = slider.value;
}
