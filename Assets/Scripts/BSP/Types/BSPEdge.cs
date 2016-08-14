using UnityEngine;
using System.Collections;

public class BSPEdge 
{
	public ushort startIndex;
	public ushort endIndex;

	public BSPEdge( ushort start, ushort end )
	{
		startIndex = start;
		endIndex = end;
	}
}
