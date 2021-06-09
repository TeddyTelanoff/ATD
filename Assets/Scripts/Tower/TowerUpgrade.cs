using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Operator: int
{
	None,
	Assign,
	Combine,
	Scale,
}

[Serializable]
public struct Operation<T>
{
	public T value;
	public Operator op;

	public T Resolve(ref T to) =>
		op switch
		{
			Operator.None => to,
			Operator.Assign => to = value,
			Operator.Combine => to = (dynamic)to + value,
			Operator.Scale => to = (dynamic)to * value,
			_ => to,

		};
	public static explicit operator T(Operation<T> op) =>
		op.value;
	public static explicit operator Operator(Operation<T> op) =>
		op.op;
}

[Serializable]
public struct Upgrade
{
	[NonSerialized]
	public Sprite sprite;
	public string name;
	public int price;

	public DartProperty props;
	public float stick;
	public float explosion;
	public float dartSpeed;
	public float reload;
	public float range;
	public float kb;
	public int dps;
	public int blast;
	public int damage;
	public int pierce;

	//public Operation<DartProperty> props;
	//public Operation<float> effectLifetime;
	//public Operation<float> reload;
	//public Operation<float> kb;
	//public Operation<int> dps;
	//public Operation<int> damage;
	//public Operation<int> pierce;
	//public Operation<int> range;
}
