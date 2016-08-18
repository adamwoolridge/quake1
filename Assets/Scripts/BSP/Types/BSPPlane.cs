using UnityEngine;
using System.Collections;
using System.IO;

public class BSPPlane
{
    public Vector3 normal;
    public float distance;
    public int type;

    public BSPPlane( BinaryReader bspFile )
    {
        normal = new Vector3( bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle() );
        distance = bspFile.ReadSingle();
        type = bspFile.ReadInt32();        
    }
}
