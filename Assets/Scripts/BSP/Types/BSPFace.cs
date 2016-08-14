using UnityEngine;
using System.Collections;
using System.IO;

public class BSPFace
{
    public ushort planeIndex;
    public ushort sideOfPlane;
    public long edgeListIndex;
    public ushort edgeCount;
    public ushort textureInfoIndex;
    public byte lightingType;
    public byte baseLight;
    public byte lightm0;
    public byte lightm1;
    public long lightmap;

	public BSPFace( BinaryReader bspFile )
	{
        planeIndex = bspFile.ReadUInt16();
        sideOfPlane = bspFile.ReadUInt16();
        edgeListIndex = bspFile.ReadInt32();
        edgeCount = bspFile.ReadUInt16();
        textureInfoIndex = bspFile.ReadUInt16();
        lightingType = bspFile.ReadByte();
        baseLight = bspFile.ReadByte();
        lightm0 = bspFile.ReadByte();
        lightm1 = bspFile.ReadByte();
        lightmap = bspFile.ReadInt32();
    }   
}
