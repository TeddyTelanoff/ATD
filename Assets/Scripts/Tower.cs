using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower: MonoBehaviour
{
	public int pierce;

	public abstract void Fire(Ant ant);
}