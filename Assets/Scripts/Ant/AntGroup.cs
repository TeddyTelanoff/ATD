using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct AntGroup
{
	public AntType type;
	public AntProperty props;
	public int count;
	public float interval;
}
