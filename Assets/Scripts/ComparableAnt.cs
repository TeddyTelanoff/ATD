using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Ant: IComparable<Ant>
{
	public int CompareTo(Ant ant) =>
		ant.nextCheckIndex.CompareTo(nextCheckIndex);
}