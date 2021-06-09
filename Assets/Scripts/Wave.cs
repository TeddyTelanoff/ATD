using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Wave
{
	public AntType type;
	public AntProperty props;
	public int count;
	public float interval;
}
