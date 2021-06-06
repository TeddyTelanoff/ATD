using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct AntTarget
{
	public static int First(Ant a, Ant b)
	{
		if (a.nextCheckIndex != b.nextCheckIndex)
			return a.nextCheckIndex.CompareTo(b.nextCheckIndex);

		return b.dir.sqrMagnitude.CompareTo(a.dir.sqrMagnitude);
	}

	public static int Last(Ant a, Ant b) =>
		First(b, a);

	public static int Strong(Ant a, Ant b)
	{
		if (a.type == b.type)
			return First(a, b);

		return a.type.CompareTo(b);
	}

	public static int Weak(Ant a, Ant b)
	{
		if (a.type == b.type)
			return First(a, b);

		return b.type.CompareTo(a);
	}
}