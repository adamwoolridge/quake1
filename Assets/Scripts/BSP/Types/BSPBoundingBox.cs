using UnityEngine;
using System.Collections;

public class BSPBoundingBox
{
    public Vector3 min;
    public Vector3 max;

    public BSPBoundingBox( Vector3 min, Vector3 max )
    {
    	this.min = min;
    	this.max = max;
    }
}
