using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public TMP_Text moneyText;
	public int Money
	{
		get => _money;
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
}
