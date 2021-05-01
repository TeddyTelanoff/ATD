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
	[SerializeField]
	private int _money;

	private void Start()
	{
		Instance = this;
		moneyText.text = $"${_money}";
	}
}
