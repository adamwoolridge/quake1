using UnityEngine;
using System.Collections;
using System.IO;

public class BSPNode
{
    public long planeIndex;
    public ushort front;
    public ushort back;
    public BSPBoundingBox boundingBox;
    public ushort faceIndex;
    public ushort faceCount;

    public BSPNode( BinaryReader bspFile )
    {
        planeIndex = bspFile.ReadInt32();
        front = bspFile.ReadUInt16();
        back = bspFile.ReadUInt16();

        int x = bspFile.ReadInt16();
        int y = bspFile.ReadInt16();
        int z = bspFile.ReadInt16();

        int x2 = bspFile.ReadInt16();
        int y2 = bspFile.ReadInt16();
        int z2 = bspFile.ReadInt16();

        boundingBox = new BSPBoundingBox(new Vector3(x, z, y), new Vector3(x2, z2, y2));

        faceIndex = bspFile.ReadUInt16();
        faceCount = bspFile.ReadUInt16();
    }
}
