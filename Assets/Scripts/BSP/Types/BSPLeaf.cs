using UnityEngine;
using System.Collections;
using System.IO;

public class BSPLeaf
{   
    public int type;
    public int visListIndex;
    public BSPBoundingBox boundingBox;
    public ushort faceListIndex;
    public ushort faceCount;
    public byte  sndWater;
    public byte sndSky;
    public byte sndSlime;
    public byte sndLava;

    public BSPLeaf( BinaryReader bspFile )
    {
        type = bspFile.ReadInt32();
        visListIndex = bspFile.ReadInt32();

        boundingBox = new BSPBoundingBox(new Vector3(bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle()),
                                  new Vector3(bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle()));

        faceListIndex = bspFile.ReadUInt16();
        faceCount = bspFile.ReadUInt16();

        sndWater = bspFile.ReadByte();
        sndWater = bspFile.ReadByte();
        sndSlime = bspFile.ReadByte();
        sndLava = bspFile.ReadByte();
    }
}
