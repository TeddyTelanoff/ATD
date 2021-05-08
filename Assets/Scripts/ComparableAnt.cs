using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Ant: IComparable<Ant>
{
	public int CompareTo(Ant ant)
	{
		if (ant.nextCheckIndex != nextCheckIndex)
			return ant.nextCheckIndex.CompareTo(nextCheckIndex);

		return dir.sqrMagnitude.CompareTo(ant.dir.sqrMagnitude);
	}
}