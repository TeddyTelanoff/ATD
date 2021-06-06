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
	public T value { get; set; }
	public Operator op { get; set; }

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
	public Sprite sprite { get; set; }
	public string name { get; set; }
	public int price { get; set; }

	public Operation<DartProperty> props { get; set; }
	public Operation<float> effectLifetime { get; set; }
	public Operation<float> reload { get; set; }
	public Operation<float> kb { get; set; }
	public Operation<int> dps { get; set; }
	public Operation<int> damage { get; set; }
	public Operation<int> pierce { get; set; }
	public Operation<int> range { get; set; }
}
