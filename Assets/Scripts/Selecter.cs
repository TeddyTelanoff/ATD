using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selecter: MonoBehaviour
{
	public UnityEvent selectable;

	private void OnMouseUpAsButton() =>
		selectable.Invoke();
}
