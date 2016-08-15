using UnityEngine;
using System.Collections;
using System.IO;

public class BSPTextureSurface
{
    public Vector3 u;
    public float uOffset;
    public Vector3 v;
    public float vOffset;
    public long textureIndex;
    public long animated;

    public BSPTextureSurface( BinaryReader bspFile )
    {
        float x = bspFile.ReadSingle();
        float y = bspFile.ReadSingle();
        float z = bspFile.ReadSingle();
        u = new Vector3( x, z, y );
        uOffset = bspFile.ReadSingle();

        x = bspFile.ReadSingle();
        y = bspFile.ReadSingle();
        z = bspFile.ReadSingle();
        v = new Vector3( x, z, y );
        vOffset = bspFile.ReadSingle();

        textureIndex = bspFile.ReadInt32();
        animated = bspFile.ReadInt32();
    }
}
