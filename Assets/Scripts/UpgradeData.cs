using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct OperationData<T>
{
	public T value;
	public string op;
}

[Serializable]
public struct Upgrade
{
	public string sprite;
	public string name;
	public int price;

	public OperationData<DartProperty> props;
	public OperationData<float> effectLifetime;
	public OperationData<float> reload;
	public OperationData<float> kb;
	public OperationData<int> dps;
	public OperationData<int> damage;
	public OperationData<int> pierce;
	public OperationData<int> range;
}
