using UnityEngine;
using System.Collections;

public class BSPModel
{
    public BSPBoundingBox boundingBox; // 24
    public Vector3 origin; //12
    public long node_id0; // 8
    public long node_id1; // 8
    public long node_id2; // 8
    public long node_id3; // 8 
    public long leafCount; // 8
    public long faceIndex; // 8
    public long faceCount; // 8
}
