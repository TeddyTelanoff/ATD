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
	public string image;
	public string name;
	public int price;

	public Operation<DartProperty> props;
	public Operation<float> effectLifetime;
	public Operation<float> reload;
	public Operation<float> kb;
	public Operation<int> dps;
	public Operation<int> damage;
	public Operation<int> pierce;
	public Operation<int> range;
}
