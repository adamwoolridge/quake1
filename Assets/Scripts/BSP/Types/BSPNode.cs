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

        boundingBox = new BSPBoundingBox(new Vector3(bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle()),
                                          new Vector3(bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle()));

        faceIndex = bspFile.ReadUInt16();
        faceCount = bspFile.ReadUInt16();
    }
}
