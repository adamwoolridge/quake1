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

        int x = bspFile.ReadInt16();
        int y = bspFile.ReadInt16();
        int z = bspFile.ReadInt16();

        int x2 = bspFile.ReadInt16();
        int y2 = bspFile.ReadInt16();
        int z2 = bspFile.ReadInt16();

        boundingBox = new BSPBoundingBox(new Vector3(x, z, y), new Vector3(x2, z2, y2));

        faceListIndex = bspFile.ReadUInt16();
        faceCount = bspFile.ReadUInt16();        
        
        sndWater = bspFile.ReadByte();
        sndWater = bspFile.ReadByte();
        sndSlime = bspFile.ReadByte();
        sndLava = bspFile.ReadByte();        
    }
}
